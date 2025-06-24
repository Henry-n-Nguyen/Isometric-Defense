using UnityEngine;

public class Tower_LaserType : IAttackStrategy
{
    public void Attack(Tower tower)
    {
        Enemy enemy = tower.CurrentTarget.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.Hit(tower.damage);

            tower.ApplyEffectsTo(enemy);
        }
    }

    public void UpdateStrategy(Tower tower)
    {
        LineRenderer lineRenderer = tower.lineRenderer;

        if (lineRenderer == null)
        {
            Debug.LogError("Line Renderer is not assigned on the Tower!");
            return;
        }

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

        lineRenderer.SetPosition(0, tower.firePoint.position);
        lineRenderer.SetPosition(1, tower.CurrentTarget.position);
    }

    public void OnNoTarget(Tower tower)
    {
        LineRenderer lineRenderer = tower.lineRenderer;

        if (lineRenderer != null && lineRenderer.enabled)
        {
            lineRenderer.enabled = false;
        }
    }
}
