using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class Tower : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;
    public Transform partToRotate;

    [Header("Stats")]
    public int damage = 1;
    public float fireRate = 1f;

    [Header("Attack Type Specifics")]
    [Tooltip("Check to specifics attack, uncheck all to use Bullets")]
    [SerializeField] private bool useLaser;
    [Tooltip("Assign Line Renderer if USING laser")]
    [SerializeField] public LineRenderer lineRenderer;

    [Space(0.5f)]
    [Tooltip("Assign Bullet Prefab if use Bullets")]
    [SerializeField] public GameObject bulletPrefab;

    [Header("Status Effects")]
    [Tooltip("Check to enable slow effect")]
    [SerializeField] private bool applySlow;

    [Tooltip("Check to enable burn effect")]
    [SerializeField] private bool applyBurn;

    [Tooltip("Check to enable stun effect")]
    [SerializeField] private bool applyStun;

    [Tooltip("Check to enable poison effect")]
    [SerializeField] private bool applyPoison;


    [Header("Tower's Ingame Stat")]
    [SerializeField] private TargetPriority targetPriority;
    [SerializeField] private List<Transform> targetList = new List<Transform>();
    [SerializeField] private Transform target;

    public Transform CurrentTarget { get { return target; } }

    private float fireCountdown = 0f;
    private IAttackStrategy currentAttackStrategy;

    private void Awake()
    {
        if (useLaser)
        {
            currentAttackStrategy = new Tower_LaserType();
        }
        else
        {
            currentAttackStrategy = new Tower_BulletType();
        }
    }

    private void Update()
    {
        if (targetList.Count == 0)
        {
            currentAttackStrategy.OnNoTarget(this);
            target = null;

            return;
        }

        LockOnTarget();

        currentAttackStrategy.UpdateStrategy(this);

        if (fireCountdown <= 0f)
        {
            currentAttackStrategy.Attack(this);
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        CleanUpTargetList();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy")) targetList.Add(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy")) targetList.Remove(collision.transform);
    }

    private void CleanUpTargetList()
    {
        targetList.RemoveAll(item => item == null);
    }

    // GET TARGET
    private void LockOnTarget()
    {
        target = GetTargetByPriority(targetPriority);

        Vector2 dir = target.position - partToRotate.position;
        float angle = Vector2.SignedAngle(transform.right, dir);
        partToRotate.rotation = Quaternion.Euler(0f, 0f, angle - 180);
    }

    private Transform GetTargetByPriority(TargetPriority priority)
    {
        switch (priority)
        {
            case TargetPriority.First: return GetFirstTarget();
            case TargetPriority.Last: return GetLastTarget();
        }

        return null;
    }

    private Transform GetFirstTarget()
    {
        Transform tmpTarget = targetList[0];

        float maxDistance = 0f;

        foreach (Transform target in targetList)
        {
            Enemy tmpEnemy = target.GetComponent<Enemy>();
            if (tmpEnemy.GetTotalDistanceMoved() > maxDistance)
            {
                tmpTarget = target;
                maxDistance = tmpEnemy.GetTotalDistanceMoved();
            }
        }

        return tmpTarget;
    }

    private Transform GetLastTarget()
    {
        return targetList[targetList.Count - 1];
    }

    // STATUS EFFECT
    public void ApplyEffectsTo(Enemy enemy)
    {
        if (applySlow && enemy.CanSlow)
        {
            enemy.ApplyStatusEffect(new StatusEffect_Slow(1, 0.5f, enemy));
        }

        if (applyBurn && enemy.CanBurn)
        {
            enemy.ApplyStatusEffect(new StatusEffect_Burn(1, 1, 0.5f, enemy));
        }

        if (applyStun && enemy.CanStun)
        {
            enemy.ApplyStatusEffect(new StatusEffect_Stun(1, enemy));
        }

        if (applyPoison && enemy.CanPoison)
        {
            enemy.ApplyStatusEffect(new StatusEffect_Poison(1, 1, enemy));
        }
    }
}
