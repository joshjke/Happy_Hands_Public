using UnityEngine;

public class Key : MonoBehaviour
{
    public SpriteRenderer sprite;
    public bool hasBeenPressed;
    public string axis;
    public bool negative;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

}
