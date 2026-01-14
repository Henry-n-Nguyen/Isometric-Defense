using System;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public event Action OnTowerDeployed;
    public event Action OnEnemyInvaded;
    public event Action OnEndLevel;

    [Space(0.5f)]
    [Header("Ingame Statitics")]
    public int Money;
    public int MaxMoney;
    public int Profit;
    public int Lives;
    
    public bool IsWinLevel;

    // private
    private float timer;

    private void Awake()
    {
        IsWinLevel = false;

        Money = CoreManager.Ins.PlayerStats.StartMoney;
        MaxMoney = CoreManager.Ins.PlayerStats.StartMaxReach;
        Profit = CoreManager.Ins.PlayerStats.StartProfit;

        Lives = CoreManager.Ins.PlayerStats.StartLives;

        WaveManager.Ins.OnInvasionEnd += OnLevelEnd;
    }

    private void Start()
    {
        UIManager.Ins.OpenUI<UI_Ingame>();
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

        if (Lives <= 0)
        {
            OnLevelEnd();
        }
    }

    public void OnLevelEnd()
    {
        WaveManager.Ins.OnInvasionEnd -= OnLevelEnd;

        OnEndLevel?.Invoke();

        //if (IsWinLevel)
        //{
        //    Debug.Log("You Win :))))");
        //    return;
        //}

        //Debug.Log("You Lose!!!");
    }
}
