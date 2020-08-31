using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD_UI : MonoBehaviour
{
    public bool isLeft;
    public SmackUI smackUI;
    public Key[] keys;
    private bool isStillOn = true;

    private AudioSource keyPressAudio;

    void Start()
    {
        keys = GetComponentsInChildren<Key>();
        keyPressAudio = GetComponent<AudioSource>();
    }

    public void StartTutorial()
    {
        foreach (Key key in keys)
        {
            key.gameObject.SetActive(true);
            key.hasBeenPressed = false;
            key.sprite.color = Color.black;
        }
        isStillOn = true;
    }


    void Update()
    {
        if (GameManager.Instance.paused)
            return;
        CheckActiveKeys();
    }

    void CheckActiveKeys()
    {
        string side = isLeft ? "Left" : "Right";
        bool allKeysActive = true;
        for (int i = 0; i < keys.Length; ++i)
        {
            if (!keys[i].hasBeenPressed)
            {
                int compareVal = keys[i].negative ? -1 : 1;
                if (Input.GetAxisRaw(side + keys[i].axis) == compareVal)
                {
                    keys[i].hasBeenPressed = true;
                    keys[i].sprite.color = Color.magenta;
                    keyPressAudio.Play();
                    keyPressAudio.pitch += 1;
                }
                else
                    allKeysActive = false;
            }
        }
        if (allKeysActive && isStillOn)
            StartTurnOffUI();
    }

    public void StartTurnOffUI()
    {
        isStillOn = false;
        foreach (Key key in keys)
            key.sprite.color = Color.green;

        smackUI.gameObject.SetActive(true);
        smackUI.CountDown();
        Invoke("TurnOffUI", 0.5f);
    }

    public void TurnOffUI()
    {
        keyPressAudio.pitch = 1;
        gameObject.SetActive(false);
    }
}
