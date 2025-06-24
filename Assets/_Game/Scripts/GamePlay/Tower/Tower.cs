using System.Collections.Generic;
using UnityEngine;

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


    [Header("Tower's Ingame Stat")]
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

    private void CleanUpTargetList()
    {
        targetList.RemoveAll(item => item == null);
    }

    private void LockOnTarget()
    {
        target = targetList[0];

        Vector2 dir = target.position - partToRotate.position;
        float angle = Vector2.SignedAngle(transform.right, dir);
        partToRotate.rotation = Quaternion.Euler(0f, 0f, angle - 180);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy")) targetList.Add(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy")) targetList.Remove(collision.transform);
    }

    // STATUS EFFECT
    public void ApplyEffectsTo(Enemy enemy)
    {
        if (applySlow)
        {
            enemy.ApplyStatusEffect(new StatusEffect_Slow(1, 0.5f, enemy));
        }
        if (applyBurn)
        {
            enemy.ApplyStatusEffect(new StatusEffect_Burn(1, 1, 0.5f, enemy));
        }
    }
}
