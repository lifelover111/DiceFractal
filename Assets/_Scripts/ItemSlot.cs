using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] bool isActiveSlot = false;
    public event System.Action OnSlotDrop = delegate { };
    public event System.Action OnSlotEmpty = delegate { };
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            StartCoroutine(ItemDroppedCoroutine());
        }
    }
    private void FixedUpdate()
    {
        if(isActiveSlot && transform.childCount == 0)
        {
            OnSlotEmpty?.Invoke();
        }
    }
    IEnumerator ItemDroppedCoroutine()
    {
        yield return new WaitWhile(() => { return transform.childCount == 0; });
        OnSlotDrop?.Invoke();
        if(isActiveSlot)
            FightController.instance.clickSound.Play();
    }
}
