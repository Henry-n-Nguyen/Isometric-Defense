using UnityEngine;
using System.Collections.Generic;

public class CoreManager : Singleton<CoreManager>
{
    [Header("Stats")]
    public PlayerStats PlayerStats;

    public Transform levelHolder;
    public List<Level> levelList = new List<Level>();

    void Start()
    {
        UIManager.Ins.OpenUI<UI_LevelSelect>();
    }

    void Update()
    {
        
    }

    public void LevelSpawn(int index)
    {
        Instantiate(levelList[index], levelHolder);

        UIManager.Ins.CloseDirectly<UI_LevelSelect>();
    }
}
