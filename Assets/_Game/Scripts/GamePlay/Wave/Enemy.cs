using System;
using UnityEngine;
using HuySpace;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    //public event Action OnDeath;

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
    [Space(0.5f)]
    [SerializeField] private float totalDistanceMoved;

    [Header("Resistants")]
    [Tooltip("Check to enable slow resistant")]
    [SerializeField] private bool slowResistant;
    public bool CanSlow => !slowResistant;

    [Tooltip("Check to enable burn resistant")]
    [SerializeField] private bool burnResistant;
    public bool CanBurn => !burnResistant;

    [Tooltip("Check to enable stun resistant")]
    [SerializeField] private bool stunResistant;
    public bool CanStun => !stunResistant;

    [Tooltip("Check to enable poison resistant")]
    [SerializeField] private bool poisonResistant;
    public bool CanPoison => !poisonResistant;

    [Header("Status Variables")]
    [SerializeField] private List<StatusEffect> activeEffects = new List<StatusEffect>();
    [SerializeField] private StatusEffect[] effects;
    [SerializeField] private int poisonStack = 0;

    // Move variables
    private Transform target;
    private int waypointIndex = 0;
    private Transform[] points;

    private bool isInitialized;

    private void Awake()
    {
        isInitialized = false;
        OnInit();
    }

    private void Update()
    {
        if (!isInitialized) return;

        Vector3 dir = target.position - transform.position;
        Vector3 movementVector = dir.normalized * speed * Time.deltaTime;

        float distanceThisFrame = movementVector.magnitude;
        totalDistanceMoved += distanceThisFrame;

        transform.Translate(movementVector, Space.World);
    
        if (Vector3.Distance(transform.position, target.position) <= 0.02f)
        {
            GetNextWaypoint();
        }

        UpdateStatusEffects();
    }

    // MOVING
    public float GetTotalDistanceMoved()
    {
        return totalDistanceMoved;
    }

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
    public virtual void OnInit()
    {
        hp = baseHp;
        attack = baseAttack;
        speed = baseSpeed;
        moneyDrop = baseMoneyDrop;

        totalDistanceMoved = 0f;

        poisonStack = 0;

        points = WaveManager.Ins.waypoints.points[(int)enemyType];
        target = points[0];

        isInitialized = true;
    }

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

        ReleaseAllStatusEffect();

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

        activeEffects.RemoveAll(effect =>
        {
            bool shoudRemove = effect.UpdateEffect();
            if (shoudRemove) effect.EndEffect();
            return shoudRemove;
        });
    }

    private void ReleaseAllStatusEffect()
    {
        foreach (StatusEffect effect in activeEffects)
        {
            effect.EndEffect();
        }

        activeEffects.Clear();
    }

    public void OnPoisoned()
    {
        poisonStack++;

        if (poisonStack >= 2)
        {
            Hit(2);

            poisonStack = 0;
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
