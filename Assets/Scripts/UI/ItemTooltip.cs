using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public TextMeshProUGUI tooltipText;

    public void SetTooltip(string text)
    {
        if (text != null)
        {
            tooltipText.text = text;
        }
        else
        {
            tooltipText.text = "";
        }
        tooltipText.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipText)
            tooltipText.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipText)
            tooltipText.gameObject.SetActive(false);
    }
}
