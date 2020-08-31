using UnityEngine;

public class FlyController : MonoBehaviour
{
    private EnemyManager enemyManager;

    public Sprite Shield1Sprite;
    private Sprite startingSprite;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public AudioClip buzzingSound;
    public AudioClip deathSound;
    // 0 is buzzing, 1 is deathSound
    private AudioSource[] audioSources;
    private Animator anim;
    private int health = 1;
    private FlyMovement flyMovement;

    private bool wasPaused;
    public KidFlyController kidFly;

    FlyState state;
    enum FlyState
    {
        Alive,
        InAnimation,
        DeathPause,
        Dead
    }

    void Start()
    {
        ComponentSetup();
        Revive();
    }

    private void ComponentSetup()
    {
        flyMovement = GetComponent<FlyMovement>();
        anim = GetComponent<Animator>();
        audioSources = GetComponents<AudioSource>();
        Debug.Assert(audioSources[0] != null);
        Debug.Assert(audioSources[1] != null);
        audioSources[0].clip = buzzingSound;
        audioSources[1].clip = deathSound;

        enemyManager = GetComponentInParent<EnemyManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startingSprite = spriteRenderer.sprite;
        rb = GetComponent<Rigidbody2D>();
        wasPaused = GameManager.Instance.paused;

    }

    void Update()
    {       
        if (GameManager.Instance.paused)
        {
            if (!wasPaused)
            {
                audioSources[0].Pause();
                wasPaused = true;
            }
            return;

        }
        else if (wasPaused)
        {
            audioSources[0].UnPause();
            wasPaused = false;
        }
        
        switch (state)
        {
            case FlyState.Alive:
                flyMovement.UpdateIdleFlight();
                break;
            case FlyState.InAnimation:
                flyMovement.UpdateCircleFlight();
                break;
            case FlyState.DeathPause:
                break;
            case FlyState.Dead:
                DropDead();
                break;
        }
    }

    public void DamageFly()
    {
        --health;
        if (health <= 0)
            KillFly();
        else
            DropShield();
    }

    private void DropShield()
    {
        spriteRenderer.sprite = startingSprite;
    }

    private void KillFly()
    {
        ComponentSetup();
        anim.SetBool("StealthMode", false);
        spriteRenderer.sprite = startingSprite;
        spriteRenderer.color = Color.white;
        audioSources[0].Stop();
        audioSources[1].pitch = Random.Range(0.5f, 3f);
        audioSources[1].Play();
        state = FlyState.DeathPause;
        transform.up = Vector3.down;
        Invoke("PauseBeforeDying", 1f);
    }

    private void PauseBeforeDying()
    {
        state = FlyState.Dead;
        rb.gravityScale = 5f;
    }

    private void DropDead()
    {
        if (Camera.main.WorldToViewportPoint(transform.localPosition).y < 0)
        {
            if (kidFly != null)
                enemyManager.EnemyDead();
            gameObject.SetActive(false);
        }
    }

    public void Revive()
    {
        health = 1;
        state = FlyState.Alive;
        if (anim == null)
            ComponentSetup();
        anim.SetBool("StealthMode", false);
        spriteRenderer.color = Color.white;
        audioSources[0].PlayDelayed(Random.Range(0, 2f));
        rb.gravityScale = 0f;
        

        transform.right = Vector3.right;
        transform.up = Vector3.up;
    }

    public void StartStealthMode()
    {
        anim.SetBool("StealthMode", true);
    }

    public void StartShield1Mode()
    {
        spriteRenderer.sprite = Shield1Sprite;
        health = 2;
    }
    public void ActivateKidFly()
    {
        if (kidFly != null)
        {
            kidFly.gameObject.SetActive(true);
            kidFly.Revive();
        }
    }
    public void DeactivateKidFly()
    {
        if (kidFly != null)
        {
            kidFly.gameObject.SetActive(false);
        }
    }
    public void StartAnimation()
    {
        state = FlyState.InAnimation;
    }
    public void StopAnimation()
    {
        state = FlyState.Alive;
    }

}
