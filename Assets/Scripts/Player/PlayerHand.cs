using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    private Transform Head;
    public bool IsLeftHand;

    public float Radius;
    private Vector2 centerOffset;

    private Collider2D collider;
    private bool colliderOn;
    private float colliderTimeMax = 0.5f;
    private float colliderTimeCurrent;
    private PlayerHandCharge handCharge;

    private bool restrained = false;

    private AudioSource audioSource;

    void Start()
    {
        Head = transform.root;
        centerOffset = transform.localPosition;
        collider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        handCharge = GetComponentInParent<PlayerHandCharge>();
    }

    void Update()
    {
        if (GameManager.Instance.paused)
            return;

        if (restrained)
            return;

        string hand =  IsLeftHand ? "Left" : "Right";

        float horizontal = Input.GetAxisRaw(hand + "Horizontal");
        float vertical = Input.GetAxisRaw(hand + "Vertical");
        Vector2 displacement = new Vector2(horizontal, vertical);
        displacement *= Radius;
        transform.localPosition = centerOffset + displacement;

        CheckCollider();
    }

    private void CheckCollider()
    {
        // collision off
        collider.isTrigger = true;
        // if hands are closed
        if (Input.GetAxisRaw("LeftHorizontal") == 1 && (Input.GetAxisRaw("RightHorizontal")) == -1)
        {
            float pitch;
            if (colliderTimeCurrent == colliderTimeMax)
            {
                // charge attack condition
                if (handCharge.charged > 0)
                {
                    pitch = Random.Range(2f, 3f);
                    handCharge.ResetCharge();
                    gameObject.transform.localScale = new Vector2(1.25f, 1.25f);
                }
                else
                {
                    pitch = Random.Range(4f, 6f);
                }
                audioSource.pitch = pitch;
                audioSource.Play();
            }

            colliderTimeCurrent -= Time.deltaTime;
            // if closed for too long
            if (colliderTimeCurrent <= 0)
                collider.isTrigger = true;
            else
                collider.isTrigger = false;
        }
        else
        {
            // reset closed timer
            colliderTimeCurrent = colliderTimeMax;
            gameObject.transform.localScale = new Vector2(1f, 1f);
        }
    }

    public void Restrain()
    {
        restrained = true;
    }

    public void UnRestrain()
    {
        restrained = false;
    }

}
