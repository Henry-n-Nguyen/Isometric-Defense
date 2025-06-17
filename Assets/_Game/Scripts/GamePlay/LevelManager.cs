using System;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Stats")]
    public PlayerStats PlayerStats;

    public event Action OnTowerDeployed;
    public event Action OnEnemyInvaded;

    [Space(0.5f)]
    [Header("Ingame Statitics")]
    public int Money;
    public int MaxMoney;
    public int Profit;

    public int Lives;

    private float timer;

    private void Start()
    {
        Money = PlayerStats.StartMoney;
        MaxMoney = PlayerStats.StartMaxReach;
        Profit = PlayerStats.StartProfit;

        Lives = PlayerStats.StartLives;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (Money < MaxMoney)
        {
            if (timer >= 0.2f)
            {
                Money += Profit;
                timer = 0f;
            }
        }
    }

    public void OnMoneyChanged(int quantity)
    {
        Money -= quantity;
        OnTowerDeployed?.Invoke();
    }

    public void OnInvaded(int amount)
    {
        Lives -= amount;
        OnEnemyInvaded?.Invoke();
    }
}
