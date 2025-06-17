using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<ShopItem> shopItems = new List<ShopItem>();

    private int selectedItemId;

    private void Start()
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            if (i < BuildManager.Ins.TowerBlueprints.Count) shopItems[i].tower = BuildManager.Ins.TowerBlueprints[i];
            else shopItems[i].tower = null;
        }

        LevelManager.Ins.OnTowerDeployed += CooldownSelectedItem;
    }

    public void SelectItem(int id)
    {
        BuildManager.Ins.SetTowerToBuild(id);

        selectedItemId = id;

        shopItems[id].OnClick();
    }

    public void CooldownSelectedItem()
    {
        shopItems[selectedItemId].OnCooldown();
    }
}
