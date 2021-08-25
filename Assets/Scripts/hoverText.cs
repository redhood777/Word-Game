using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class hoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hover;

    public void Start()
    {
        hover.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover.SetActive(false);
    }
}
