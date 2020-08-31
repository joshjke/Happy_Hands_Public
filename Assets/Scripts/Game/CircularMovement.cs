using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public float VertDist;
    public float HoriDist;
    public float Speed;
    private Vector2 center;

    void Start()
    {
        center = transform.position;
    }

    void Update()
    {
        float x = center.x + HoriDist * Mathf.Sin(Time.fixedTime * Speed);
        float y = center.y + VertDist * Mathf.Cos(Time.fixedTime * Speed);
        transform.localPosition = new Vector2(x, y);
    }
}
