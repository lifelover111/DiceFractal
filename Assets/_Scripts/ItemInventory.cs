using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Item item;
    Image icon;

    private void Awake()
    {
        icon = GetComponent<Image>();
    }
    public void SetItem(Item item)
    {
        this.item = item;
        icon.sprite = item.icon;
        if (item.IsDisposable())
            item.OnUse += () => { if(gameObject != null) Destroy(gameObject); };
    }

    public Item GetItem()
    {
        return item;
    }


    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    if (GameInfo.instance.transform.gameObject.activeInHierarchy)
    //        GameInfo.instance.StartCoroutine(GameInfo.instance.Close());
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameInfo.instance.gameObject.SetActive(true);

        Vector2 pos = transform.position + Vector3.up * 600 + Vector3.right * 1150;
   
        GameInfo.instance.SetInfo(pos, item.name, item.GetDescription());
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameInfo.instance.gameObject.activeInHierarchy)
        {
            GameInfo.instance.Close(); 
            GameInfo.instance.gameObject.SetActive(false);
        }
    }

}
