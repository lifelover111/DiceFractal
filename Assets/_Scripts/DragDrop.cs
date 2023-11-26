using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    public RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField]private RectTransform originalParent;
    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        originalParent = transform.parent.GetComponent<RectTransform>();
        canvas = transform.root.GetComponent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!FightController.instance.playerTurn)
            return;
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        GameInfo.instance.gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!FightController.instance.playerTurn)
            return;
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!FightController.instance.playerTurn)
            return;
        //Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("Slot"))
        {
            if (eventData.pointerEnter.transform.childCount > 0)
            {
                Transform anotherItem = eventData.pointerEnter.transform.GetChild(0);
                anotherItem.GetComponent<DragDrop>().SetParent(originalParent);
            }
            SetParent(eventData.pointerEnter.GetComponent<RectTransform>());

        }
        else if(eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("Item"))
        {
            RectTransform parent = eventData.pointerEnter.transform.parent.GetComponent<RectTransform>();
            RectTransform anotherItem = eventData.pointerEnter.GetComponent<RectTransform>();
            anotherItem.GetComponent<DragDrop>().SetParent(originalParent);

            SetParent(parent);
        }
        else
        {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    public void SetParent(RectTransform parent)
    {
        transform.SetParent(parent);
        originalParent = parent; 
        rectTransform.anchoredPosition3D = Vector3.zero;
    }
    

}
