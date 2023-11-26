using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [SerializeField] private Canvas canvas;
    public RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Transform originalParent;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        originalParent = transform.parent;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

     
        if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<ItemSlot>() != null)
        {
            
            originalParent = eventData.pointerEnter.transform;
            transform.SetParent(eventData.pointerEnter.transform);
            rectTransform.anchoredPosition = Vector2.zero; 
        }
        else
        {
            
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = Vector2.zero; 
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

}
