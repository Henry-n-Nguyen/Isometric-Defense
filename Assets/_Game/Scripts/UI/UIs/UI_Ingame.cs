using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Ingame : UICanvas
{
    public GameObject _pauseUI;
    public GameObject _resultUI;

    [SerializeField] private TMP_Text currencyText;
    [SerializeField] private TMP_Text liveText;

    public override void Open()
    {
        base.Open();

        LevelManager.Ins.OnEnemyInvaded += UpdateLiveText;
        LevelManager.Ins.OnEndLevel += EndLevel;

        UpdateLiveText();
    }

    public override void CloseDirectly()
    {
        LevelManager.Ins.OnEnemyInvaded -= UpdateLiveText;

        base.CloseDirectly();
    }

    private void FixedUpdate()
    {
        UpdateCurrency();
    }

    private void UpdateCurrency()
    {
        currencyText.text = (LevelManager.Ins.Money + "/" + LevelManager.Ins.MaxMoney).ToString();
    }

    private void UpdateLiveText()
    {
        liveText.text = LevelManager.Ins.Lives.ToString();
    }

    public void Pause()
    {
        _pauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        _pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void EndLevel()
    {
        _resultUI.SetActive(true);
    }

    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
}
