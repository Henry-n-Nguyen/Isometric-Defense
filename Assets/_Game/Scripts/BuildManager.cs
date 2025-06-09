using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : Singleton<BuildManager>
{
    public event Action OnCurrencyChanged;

    [Header("Stats")]
    public PlayerStats PlayerStats;

    [Space(0.5f)]
    [Header("Input")]
    public List<TowerBlueprint> TowerBlueprint = new List<TowerBlueprint>();
    public Transform towerHolder;

    private TowerBlueprint towerToBuild;

    public bool CanBuild { get { return towerToBuild != null; } }

    private void Start()
    {
        ClearSellect();

        UIManager.Ins.OpenUI<UI_Ingame>();
    }

    public void SetTowerToBuild(int id)
    {
        if (PlayerStats.Money < TowerBlueprint[id].cost)
        {
            Debug.Log("Insufficien!!!");
            return;
        }

        if (id > TowerBlueprint.Count)
        {
            return;
        }

        towerToBuild = TowerBlueprint[id];
    }

    public void BuildTowerOn(Node node)
    {
        GameObject tower = Instantiate(towerToBuild.prefab, node.GetPositionToBuild(), Quaternion.identity, towerHolder);
        node.tower = tower;

        PlayerStats.Money -= towerToBuild.cost;
        OnCurrencyChanged?.Invoke();

        ClearSellect();
    }

    public void ClearSellect()
    {
        towerToBuild = null;
    }
}
