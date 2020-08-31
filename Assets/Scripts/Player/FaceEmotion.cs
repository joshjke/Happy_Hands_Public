using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceEmotion : MonoBehaviour
{
    public bool flipSprite;
    private SpriteRenderer sprite;
    public Sprite Happy;
    public Sprite Angry;
    public Sprite Sad;
    public Sprite Joy;
    public Sprite Misch;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        Debug.Assert(Happy != null);
        Debug.Assert(Angry != null);
        Debug.Assert(Sad   != null);
        Debug.Assert(Joy   != null);
        Debug.Assert(Misch != null);
    }

    void Update()
    {
        int leftHori  = (int)Input.GetAxisRaw("LeftHorizontal");
        int rightHori = (int)Input.GetAxisRaw("RightHorizontal");
        int leftVert  = (int)Input.GetAxisRaw("LeftVertical");
        int rightVert = (int)Input.GetAxisRaw("RightVertical");

        // double speed
        if (leftHori + rightHori == 2 || leftHori + rightHori == -2
                || leftVert + rightVert == 2 || leftVert + rightVert == -2)
            SetJoy();
        // closed hands
        else if (leftHori == 1 && rightHori == -1)
            SetAngry();
        // no action
        else if (leftHori == 0 && rightHori == 0)
            SetHappy();
        // open hands
        else if (leftHori == -1 && rightHori == 1)
            SetMisch();
        
        else
            SetHappy();
    }

    public void SetHappy() {sprite.sprite = Happy; if (flipSprite) sprite.flipX = true; sprite.color = Color.black; }
    public void SetAngry() {sprite.sprite = Angry; if (flipSprite) sprite.flipX = true; sprite.color = Color.black; }
    public void SetSad()   {sprite.sprite = Sad;   if (flipSprite) sprite.flipX = true; sprite.color = Color.black; }
    public void SetJoy()   {sprite.sprite = Joy;   if (flipSprite) sprite.flipX = true; sprite.color = Color.black; }
    public void SetMisch() {sprite.sprite = Misch; if (flipSprite) sprite.flipX = true; sprite.color = Color.black; }
}
