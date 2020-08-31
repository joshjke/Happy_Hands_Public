using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmackUI : MonoBehaviour
{    private int UIDownCount = 2;
    private Animator anim;

    private int animLoopCount = 0;
    private bool smackPerformed = false;
    private bool showingSmackUI = false;
    private bool endSelf = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (showingSmackUI)
        {
            if (Input.GetAxisRaw("LeftHorizontal") == 1
                && Input.GetAxisRaw("RightHorizontal") == -1)
                smackPerformed = true;
        }
        if (smackPerformed && animLoopCount >= 1)
        {
            endSelf = true;
        }

    }

    public void CountDown()
    {
        --UIDownCount;
        if (UIDownCount <= 0)
        {
            Invoke("StartSmackUI", 1f);
        }
    }

    public void StartSmackUI()
    {
        anim.SetTrigger("StartSmackUI");
        showingSmackUI = true;
    }

    public void SmackAnimHasLooped()
    {
        ++animLoopCount;
        if (endSelf)
        {
            GameManager.Instance.StartNextRound();
            gameObject.SetActive(false);
        }
    }

    public void StartTutorial()
    {
        UIDownCount = 2;
        animLoopCount = 0;
        smackPerformed = false;
        showingSmackUI = false;
        endSelf = false;
        if (anim == null)
            anim = GetComponent<Animator>();
        anim.SetTrigger("StopSmackUI");
    }
}
