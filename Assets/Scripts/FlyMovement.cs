using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    // the lengths between peak and valley
    public float VerticalLength;
    public float HorizontalLength;

    // how fast the fly progresses
    public float HorizontalDistanceSpeed;
    // the back and forth movement speed
    public float BackAndForthSpeed;

    private Vector2 center;
    // how far the fly has already progressed over time
    private float progressionOffsetX = 0;
    private float randomSinOffset;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        center = new Vector2(transform.localPosition.x, transform.localPosition.y);
        RandomizeFlightValues();
    }

    public void RandomizeFlightValues()
    {
        transform.localPosition = new Vector2(center.x, center.y);

        randomSinOffset = Random.Range(0, 2 * Mathf.PI);

        // the lengths between peak and valley
        VerticalLength = Random.Range(12f, 35f);
        HorizontalLength = Random.Range(0.5f, 5f);

        // how fast the fly progresses
        HorizontalDistanceSpeed = Random.Range(13f, 50f);

        // face the main movement direction
        spriteRenderer.flipX = HorizontalDistanceSpeed < 0;

        // the back and forth movement speed
        BackAndForthSpeed = Random.Range(12f, 40f);
    }

    public void UpdateIdleFlight()
    {
        // the greater horizontal movement over time
        progressionOffsetX += HorizontalDistanceSpeed * Time.deltaTime;
        // the quick back and forth movement
        float backForthMovement = HorizontalLength * Mathf.Sin(Time.fixedTime * BackAndForthSpeed + randomSinOffset);
        float newX = center.x + progressionOffsetX + backForthMovement;

        // vertical movement as a sin wave, only going up and down
        float newY = center.y + VerticalLength * Mathf.Sin(Time.fixedTime + randomSinOffset);
        transform.localPosition = new Vector2(newX, newY);

        // if outside the camera, flip directions
        float positionInViewportX = Camera.main.WorldToViewportPoint(transform.position).x;
        if (positionInViewportX < 0 || positionInViewportX > 1)
        {
            HorizontalDistanceSpeed *= -1;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    public void UpdateCircleFlight()
    {
        float x = center.x + 1f * Mathf.Sin(Time.fixedTime * HorizontalDistanceSpeed);
        float y = center.y + 1f * Mathf.Cos(Time.fixedTime * HorizontalDistanceSpeed);
        transform.localPosition = new Vector2(x, y);
    }
}
