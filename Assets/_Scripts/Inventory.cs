using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    [SerializeField] List<RectTransform> slots;
    [SerializeField] Scrollbar scrollbar;
    [SerializeField] Image scrollbarImage;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] GameObject slotPrefab;
    [SerializeField] RectTransform inventoryRectTransform;
    bool scrollbarIsActive = false;
    

    private void Awake()
    {
        instance = this;
    }

    public void AddItem(Item item)
    {
        if (slots.Where(s => s.childCount == 0).Count() == 0)
        {
            inventoryRectTransform.offsetMin = new Vector2(inventoryRectTransform.offsetMin.x, inventoryRectTransform.offsetMin.y - 400);
            for(int i = 0; i < 3; i++)
            {
                GameObject go = Instantiate(slotPrefab, inventoryRectTransform);
                go.transform.localScale = Vector3.one;
                slots.Add((RectTransform)go.transform);
            }
            if(!scrollbarIsActive)
            {
                scrollbarIsActive = true;
                scrollbar.enabled = true;
                scrollbarImage.enabled = true;
                scrollRect.enabled = true;
            }
        }

        GameObject itemInventory = Instantiate(GameManager.instance.inventoryItemPrefab);
        itemInventory.transform.SetParent(slots.Where(s => s.childCount == 0).FirstOrDefault());
        RectTransform itemRect = itemInventory.GetComponent<RectTransform>();
        itemRect.anchoredPosition3D = Vector3.zero;
        itemRect.localScale = Vector3.one;
        itemInventory.GetComponent<ItemInventory>().SetItem(item);
    }
}
