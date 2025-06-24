using UnityEngine;

public class Tower_BulletType : IAttackStrategy
{
    public void Attack(Tower tower)
    {
        GameObject bulletPrefab = tower.bulletPrefab;

        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab is not assigned on the Tower!");
            return;
        }

        GameObject bulletGo = Object.Instantiate(bulletPrefab, tower.firePoint.position, tower.firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Init(tower.damage, tower);
            bullet.Seek(tower.CurrentTarget);
        }
    }

    public void UpdateStrategy(Tower tower) { }

    public void OnNoTarget(Tower tower) { }
}
