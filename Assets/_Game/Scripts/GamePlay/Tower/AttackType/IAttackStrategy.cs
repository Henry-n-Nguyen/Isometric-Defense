using UnityEngine;

public interface IAttackStrategy
{
    void Attack(Tower tower);

    void UpdateStrategy(Tower tower);

    void OnNoTarget(Tower tower);
}
