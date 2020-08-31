using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtAnimationEvents : MonoBehaviour
{
    public GameObject RealPlayer;
    public GameObject CutscenePlayer;
    public GameObject EscapeUI;
    public AudioClip DoorOpenSound;
    public AudioClip JudgeNoSound;
    public AudioClip ChainBreakSound;
    public AudioClip ChainShakeSound;
    public AudioClip PolaroidShuffleSound;
    private AudioSource[] Oohs;


    private bool allowEscape = false;
    private AudioSource audioSource;

    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Oohs = GetComponentsInChildren<AudioSource>();
        if (RealPlayer != null)
            RealPlayer.SetActive(false);
        CutscenePlayer.SetActive(true);
        EscapeUI.SetActive(false);
    }

    private void Update()
    {
        // open hands and after main animation
        if (allowEscape && 
            Input.GetAxisRaw("LeftHorizontal") == -1 && Input.GetAxisRaw("RightHorizontal") == 1)
        {
            BreakFree();
        }
    }

    // Should be called by the animator when rocks are getting thrown
    public void AllowEscape()
    {
        allowEscape = true;
    }
    public void PromptEscape()
    {
        EscapeUI.SetActive(true);
    }
    
    public void BreakFree()
    {
        EscapeUI.SetActive(false);
        anim.SetTrigger("BreakFree");
    }
    public void PlayerStart()
    {
        RealPlayer.SetActive(true);
        CutscenePlayer.SetActive(false);
        GameManager.Instance.GameWon();
    }

    public void PlayDoorOpenSound()
    {
        audioSource.clip = DoorOpenSound;
        audioSource.Play();
    }
    public void PlayJudgeNoSound()
    {
        audioSource.clip = JudgeNoSound;
        audioSource.Play();
    }

    public void PlayChainBreakSound()
    {
        audioSource.clip = ChainBreakSound;
        audioSource.Play();
    }
    public void PlayPolaroidShuffleSound()
    {
        audioSource.clip = PolaroidShuffleSound;
        audioSource.Play();
    }
    public void PlayCollectiveOoh()
    {
        foreach (AudioSource ooh in Oohs)
        {
            ooh.pitch += Random.Range(-1f, 1f);
            ooh.Play();
        }
    }
    public void PlayChainShakeSound()
    {
        audioSource.clip = ChainShakeSound;
        audioSource.Play();
    }

}
