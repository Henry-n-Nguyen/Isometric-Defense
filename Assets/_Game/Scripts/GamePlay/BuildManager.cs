using System.Collections.Generic;
using UnityEngine;

public class BuildManager : Singleton<BuildManager>
{
    [Space(0.5f)]
    [Header("Input")]
    public List<TowerBlueprint> TowerBlueprints = new List<TowerBlueprint>();

    [Space(0.5f)]
    [SerializeField] private TowerBlueprint towerToBuild;
    [SerializeField] private Transform towerHolder;

    public bool CanBuild { get { return towerToBuild != null; } }

    private void Start()
    {
        ClearSelect();

        UIManager.Ins.OpenUI<UI_Ingame>();
    }

    public bool SetTowerToBuild(int id)
    {
        if (LevelManager.Ins.Money < TowerBlueprints[id].cost)
        {
            Debug.Log("Insufficient !!!");
            return false;
        }

        if (id > TowerBlueprints.Count)
        {
            return false;
        }

        towerToBuild = TowerBlueprints[id];

        return true;
    }

    public void BuildTowerOn(Node node)
    {
        GameObject tower = Instantiate(towerToBuild.prefab, node.GetPositionToBuild(), Quaternion.identity, towerHolder);
        node.tower = tower;

        LevelManager.Ins.OnMoneyChanged(towerToBuild.cost);

        ClearSelect();
    }

    public void ClearSelect()
    {
        towerToBuild = null;
    }
}
