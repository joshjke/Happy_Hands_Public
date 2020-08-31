using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool killAll = false;

    private int numEnemies;
    private int doubleEnemies;
    private FlyController[] enemies;
    private bool doubleFlyMode;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GetComponentsInChildren<FlyController>();
        doubleEnemies = enemies.Length;
        numEnemies = doubleEnemies / 2;
        foreach (FlyController enemy in enemies)
            enemy.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (killAll)
        {
            KillAll();
            killAll = false;
        }
    }


    public void EnemyDead()
    {
        --numEnemies;
        if (numEnemies <= 0)
        {
            GameManager.Instance.GameWon();
        }
    }

    public void KillAll()
    {
        foreach (FlyController enemy in enemies)
            enemy.DamageFly();
    }

        public void DeactivateAllFlies()
    {
        foreach (FlyController enemy in enemies)
        {
            enemy.DeactivateKidFly();
            enemy.gameObject.SetActive(false);
        }
    }

    public void ResetEnemies()
    {
        numEnemies = doubleEnemies / 2;
        for (int i = 0; i < numEnemies; ++i)
        {
            enemies[i].gameObject.SetActive(true);
            enemies[i].Revive();
            enemies[i].DeactivateKidFly();
        }
    }

    public void StartStealthMode()
    {
        for (int i = 0; i < numEnemies; ++i)
            enemies[i].StartStealthMode();
    }
    public void StartShield1Mode()
    {
        for (int i = 0; i < numEnemies; ++i)
            enemies[i].StartShield1Mode();
    }
    public void StartDoubleFlyMode()
    {
        numEnemies = doubleEnemies;
        for (int i = 0; i < doubleEnemies; ++i)
        {
            enemies[i].gameObject.SetActive(true);
            enemies[i].Revive();
        }
    }

    public void StartBringYourKidMode()
    {
        for (int i = 0; i < numEnemies; ++i)
            enemies[i].ActivateKidFly();
    }

    public Transform GetNewTarget()
    {
        foreach (FlyController enemy in enemies)
        {
            if (enemy.isActiveAndEnabled)
                return enemy.transform;
        }
        return null;
    }
}
