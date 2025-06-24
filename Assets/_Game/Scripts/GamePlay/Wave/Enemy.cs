using System;
using UnityEngine;
using HuySpace;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public event Action OnDeath;

    [SerializeField] private EnvironmentId enemyType;

    [Header("Base Stats")]
    [SerializeField] private int baseHp = 1;
    [SerializeField] private int baseAttack = 1;
    [SerializeField] private float baseSpeed = 10f;
    [SerializeField] private int baseMoneyDrop = 10;

    [Header("Ingame Stats")]
    [SerializeField] private int hp = 1;
    [SerializeField] private int attack = 1;
    [SerializeField] private float speed = 10f;
    [SerializeField] private int moneyDrop = 10;

    // Status Variables
    [SerializeField] private List<StatusEffect> activeEffects = new List<StatusEffect>();

    // Move variables
    private Transform target;
    private int waypointIndex = 0;
    private Transform[] points;

    private void Start()
    {
        hp = baseHp;
        attack = baseAttack;
        speed = baseSpeed;
        moneyDrop = baseMoneyDrop;

        points = Waypoints.points[(int)enemyType];
        target = points[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    
        if (Vector3.Distance(transform.position, target.position) <= 0.02f)
        {
            GetNextWaypoint();
        }

        UpdateStatusEffects();
    }

    // MOVING
    public virtual void GetNextWaypoint()
    {
        if (waypointIndex >= points.Length - 1)
        {
            EndPath();
            return;
        }

        waypointIndex++;
        target = points[waypointIndex];
    }

    private void EndPath()
    {
        LevelManager.Ins.OnInvaded(attack);
        Destroy(gameObject);
    }

    // BEHAVIOUR
    public virtual void Hit(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        int money = UnityEngine.Random.Range(moneyDrop, moneyDrop + 5);

        LevelManager.Ins.Money = LevelManager.Ins.Money + money < LevelManager.Ins.MaxMoney ? LevelManager.Ins.Money + money
                                                                                            : LevelManager.Ins.MaxMoney;

        WaveManager.Ins.ReduceEnemyNum();

        Destroy(gameObject);
    }

    // STATUS EFFECT
    public void ApplyStatusEffect(StatusEffect effect)
    {
        activeEffects.Add(effect);
        effect.ApplyEffect();
    }

    private void UpdateStatusEffects()
    {
        // if no status effect active, return
        if (activeEffects.Count == 0) return;

        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            if (activeEffects[i].UpdateEffect())
            {
                activeEffects[i].EndEffect();
                activeEffects.RemoveAt(i);
            }
        }
    }

    public void UpdateSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public float GetBaseSpeed()
    {
        return baseSpeed;
    }
}
