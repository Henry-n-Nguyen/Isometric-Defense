using TMPro;
using UnityEngine;

public class UI_Ingame : UICanvas
{
    [SerializeField] private TMP_Text currencyText;

    private PlayerStats playerStats;

    public override void Open()
    {
        base.Open();

        playerStats = BuildManager.Ins.PlayerStats;

        BuildManager.Ins.OnCurrencyChanged += UpdateCurrency;

        UpdateCurrency();
    }

    public override void CloseDirectly()
    {
        BuildManager.Ins.OnCurrencyChanged -= UpdateCurrency;

        base.CloseDirectly();
    }

    private void UpdateCurrency()
    {
        currencyText.text = (playerStats.Money + "/" + playerStats.MaxReach).ToString();
    }
}
