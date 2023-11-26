using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            RectTransform draggedRectTransform = eventData.pointerDrag.GetComponent<RectTransform>();
            RectTransform slotRectTransform = GetComponent<RectTransform>();

            // ������������� ������� draggedRectTransform ������������ slotRectTransform
            draggedRectTransform.position = slotRectTransform.position;
        }
    }
}
