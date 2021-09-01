using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class hoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hover;

    public static bool hintscore;

    public void Start()
    {
        hover.SetActive(false);
        hintscore = true;
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
