using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LevelSelect : UICanvas
{
    private int currentLevelIndex = 0;

    private void Update()
    {
        GatherInput();
    }

    private void GatherInput()
    {

    }

    public override void Open()
    {
        base.Open();
    }

    public void OnNextButtonClicked()
    {
        currentLevelIndex++;
    }

    public void OnPreviousButtonClicked()
    {
        currentLevelIndex--;
    }

    public void OnBattleButtonClicked()
    {
        CoreManager.Ins.LevelSpawn(currentLevelIndex);
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
}
