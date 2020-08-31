using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidFlyController : MonoBehaviour
{
    public Sprite Shield1Sprite;
    private Sprite startingSprite;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public AudioClip buzzingSound;
    public AudioClip deathSound;
    // 0 is buzzing, 1 is deathSound
    private AudioSource[] audioSources;
    private Animator anim;
    private FlyMovement flyMovement;

    private bool wasPaused;

    FlyState state;
    enum FlyState
    {
        Alive,
        DeathPause,
        Dead
    }

    void Start()
    {
        ComponentSetup();
        Revive();
        wasPaused = GameManager.Instance.paused;
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

        spriteRenderer = GetComponent<SpriteRenderer>();
        startingSprite = spriteRenderer.sprite;
        rb = GetComponent<Rigidbody2D>();
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
        KillFly();
    }

    private void KillFly()
    {
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
            gameObject.SetActive(false);
        }
    }

    public void Revive()
    {
        state = FlyState.Alive;
        if (spriteRenderer == null)
            ComponentSetup();
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
    }
}
