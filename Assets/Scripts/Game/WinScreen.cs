using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    private MenuFlySquish MenuFly;

    [TextArea]
    public List<string> EndScreenMessages;
    [TextArea]
    public List<string> FinalMessages;

    private List<string> endScreenMessagePool;
    private int endScreenMessageIndex = 0;
    private int finalMessageIndex = 0;

    private void Start()
    {
        MenuFly = GetComponentInChildren<MenuFlySquish>();
    }

    public void EndWinScreen()
    {
        SetNewEndMessage();
        gameObject.SetActive(false);
    }

    public void ReviveMenuFly()
    {
        if (MenuFly != null)
            MenuFly.gameObject.SetActive(true);
    }

    // random message
    private void SetNewEndMessage()
    {
        if (endScreenMessagePool == null)
            ResetMessagePools();
        if (endScreenMessageIndex >= endScreenMessagePool.Count)
        {
            SetNewFinalMessage();
            return;
        }

        textMeshPro.text = endScreenMessagePool[endScreenMessageIndex++];
    }

    // sequential messages after exhausting random messages
    private void SetNewFinalMessage()
    {
        if (finalMessageIndex >= FinalMessages.Count)
        {
            ResetMessagePools();
            SetNewFinalMessage();
            return;
        }
        if (textMeshPro != null)
            textMeshPro.text = FinalMessages[finalMessageIndex++];
    }

    // reset
    private void ResetMessagePools()
    {
        endScreenMessagePool = new List<string>(EndScreenMessages);
        ShuffleEndMessages();
        endScreenMessageIndex = 0;
        finalMessageIndex = 0;
    }

    private void ShuffleEndMessages()
    {
        for (int i = 0; i < endScreenMessagePool.Count; i++)
        {
            int rand = Random.Range(0, i);
            string temp = endScreenMessagePool[i];
            endScreenMessagePool[i] = endScreenMessagePool[rand];
            endScreenMessagePool[rand] = temp;
        }
    }

}
