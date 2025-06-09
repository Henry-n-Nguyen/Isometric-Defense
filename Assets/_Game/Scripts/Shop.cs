using UnityEngine;

public class Shop : MonoBehaviour
{
    public void SelectItem(int id)
    {
        BuildManager.Ins.SetTowerToBuild(id);
    }
}
