using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollDetector : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public static bool isScrolling = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        isScrolling = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isScrolling = false;
    }
}
