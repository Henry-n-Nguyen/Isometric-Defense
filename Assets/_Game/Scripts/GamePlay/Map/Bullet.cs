using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private Tower sourceTower;

    public float speed = 5f;
    private int damage = 1;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public void Init(int damage, Tower sourceTower)
    {
        this.damage = damage;
        this.sourceTower = sourceTower;
    }

    public void Seek(Transform _target)
    {
        target = _target;
        
    }

    private void HitTarget()
    {
        Enemy enemy = target.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.Hit(damage);

            sourceTower.ApplyEffectsTo(enemy);
        }

        Destroy(gameObject);
    }
}
