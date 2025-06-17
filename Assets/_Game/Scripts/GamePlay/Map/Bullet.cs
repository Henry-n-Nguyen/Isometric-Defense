using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 5f;

    void Update()
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

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void HitTarget()
    {
        target.GetComponent<Enemy>().Hit();
        Destroy(gameObject);
    }
}
