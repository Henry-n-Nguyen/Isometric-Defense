using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MainMenu : UICanvas
{
    [SerializeField] private GameObject _credit;

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

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void OnCreditButtonClicked()
    {
        _credit.SetActive(true);
    }

    public void OnCloseCreditButtonClicked()
    {
        _credit.SetActive(false);
    }
}
