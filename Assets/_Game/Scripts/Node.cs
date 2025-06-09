using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Color hoverColor;
    public Vector3 positionOffset;

    public GameObject tower;

    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public Vector3 GetPositionToBuild()
    {
        return transform.position + positionOffset;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!BuildManager.Ins.CanBuild)
        {
            return;
        }

        rend.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rend.color = Color.white;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!BuildManager.Ins.CanBuild)
        {
            return;
        }

        rend.color = Color.blue;

        BuildManager.Ins.BuildTowerOn(this);
    }
}
