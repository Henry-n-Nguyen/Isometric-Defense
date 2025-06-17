using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public List<Transform> targetList = new List<Transform>();
    public Transform target;

    public Transform partToRotate;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    void Start()
    {
        
    }

    void Update()
    {
        if (targetList.Count == 0)
        {
            return;
        }

        target = targetList[0];

        Vector2 dir = target.position - partToRotate.position;
        float angle = Vector2.SignedAngle(transform.right, dir);
        partToRotate.rotation = Quaternion.Euler(0f, 0f, angle - 180);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy")) targetList.Add(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy")) targetList.Remove(collision.transform);
    }

    private void Shoot()
    {
        GameObject bulletGo =  (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }
}
