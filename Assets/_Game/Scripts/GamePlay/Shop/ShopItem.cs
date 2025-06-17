using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public TowerBlueprint tower;

    [Space(0.5f)]
    [Header("References")]
    [SerializeField] private Image image;
    
    [Space(0.5f)]
    [SerializeField] private GameObject CD_Panel;
    [SerializeField] private GameObject CD_Count;
    
    [Space(0.5f)]
    [SerializeField] private GameObject Clicked_Panel;
    [SerializeField] private GameObject Block_Panel;
    
    [Space(0.5f)]
    [SerializeField] private TMP_Text cost_Text;

    private bool canReach;

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        if (tower == null)
        {
            return;
        }

        if (tower.cost <= LevelManager.Ins.Money)
        {
            Block_Panel.SetActive(false);
            return;
        }

        Block_Panel.SetActive(true);
    }

    public void OnInit()
    {
        if (tower == null)
        {
            return;
        }

        cost_Text.gameObject.SetActive(true);

        image.sprite = tower.prefab.GetComponent<SpriteRenderer>().sprite;

        cost_Text.text = tower.cost.ToString();
    }

    public void OnClick()
    {
        Clicked_Panel.SetActive(true);
    }

    public void OnRelease()
    {
        Clicked_Panel.SetActive(false);
    }

    public void OnCooldown()
    {
        Clicked_Panel.SetActive(false);

        StartCoroutine(Cooldown(tower.cd));
    }

    IEnumerator Cooldown(float time)
    {
        CD_Panel.SetActive(true);
        cost_Text.gameObject.SetActive(false);

        float currentTime = 0f;

        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            CD_Count.transform.localScale = new Vector3(currentTime / time, 1f, 1f);

            yield return null;
        }

        CD_Panel.SetActive(false);
        cost_Text.gameObject.SetActive(true);

        CD_Count.transform.localScale = Vector3.one;
    }
}
