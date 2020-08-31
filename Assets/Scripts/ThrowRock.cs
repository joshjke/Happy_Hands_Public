using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRock : MonoBehaviour
{
    public Transform target;
    public bool throwing = true;
    public float radius = 2f;
    public float speed = 40f;

    private Vector2 start;
    private AudioSource audioSource;
    void Start()
    {
        start = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (throwing)
        {
            if ((target.position - transform.position).sqrMagnitude < 2f)
            {
                audioSource.Play();
                transform.position = start;
            }
            else
            {
                float x, y;
                x = target.position.x > transform.position.x ? 1f : -1f;
                y = target.position.y > transform.position.y ? 1f : -1f;
                transform.position += new Vector3(x, y) * Time.deltaTime * speed;
            }
        }
    }
}
