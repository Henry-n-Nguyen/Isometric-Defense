using TMPro;
using UnityEngine;

public class UI_Ingame : UICanvas
{
    [SerializeField] private TMP_Text currencyText;
    [SerializeField] private TMP_Text liveText;

    public override void Open()
    {
        base.Open();

        LevelManager.Ins.OnEnemyInvaded += UpdateLiveText;

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
}
