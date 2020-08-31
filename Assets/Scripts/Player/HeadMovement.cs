using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    public float MoveSpeed;
    private PlayerHand[] hands;
    private bool restrained = false;
    // Start is called before the first frame update
    void Start()
    {
        hands = GetComponentsInChildren<PlayerHand>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.paused)
            return;
        if (restrained)
            return;
        UpdateHeadPosition();
        
    }

    public void UpdateHeadPosition()
    {
        float leftHorizontal = Input.GetAxisRaw("LeftHorizontal");
        float rightHorizontal = Input.GetAxisRaw("RightHorizontal");
        float horizontal;
        if (leftHorizontal == 0 && rightHorizontal == 0)
            horizontal = 0;
        else
            horizontal = leftHorizontal + rightHorizontal;

        float leftVertical = Input.GetAxisRaw("LeftVertical");
        float rightVertical = Input.GetAxisRaw("RightVertical");
        float vertical;
        if (leftVertical == 0 && rightVertical == 0)
            vertical = 0;
        else
            vertical = leftVertical + rightVertical;

        Vector2 positionInViewport = Camera.main.WorldToViewportPoint(transform.position);

        if (positionInViewport.x < 0 && horizontal < 0)
            horizontal = 0;
        if (positionInViewport.x > 1 && horizontal > 0)
            horizontal = 0;

        if (positionInViewport.y < 0 && vertical < 0)
            vertical = 0;
        if (positionInViewport.y > 1 && vertical > 0)
            vertical = 0;

        transform.position += MoveSpeed * new Vector3(horizontal, vertical);
    }

    public void RestrainPlayer()
    {
        restrained = true;
        foreach (PlayerHand playerHand in hands)
            playerHand.Restrain();
    }

    public void UnRestrainPlayer()
    {
        restrained = false;
        foreach (PlayerHand playerHand in hands)
            playerHand.UnRestrain();
    }
}
