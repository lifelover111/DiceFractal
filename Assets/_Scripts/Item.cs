using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Item", fileName = "new Item")]
public class Item : ScriptableObject
{
    [System.Serializable]
    class Cost
    {
        [SerializeField] int[] suitableDices;
        public bool Contains(int val)
        {
            return suitableDices.Contains(val);
        }
    }

    [SerializeField] bool isDisposable = false;
    [SerializeField] Cost price;
    [SerializeField] UnityEvent action;
    public Sprite icon;
    public event System.Action OnUse = () => { };
    [SerializeField] string description;

    public bool CheckPrice(int value)
    {
        return price.Contains(value);
    }
    public void Use(int dice)
    {
        OnUse?.Invoke();
        action?.Invoke();
        if(isDisposable)
            FightController.instance.currentFight.characters.Where(c => c.usableItem == this).FirstOrDefault().usableItem = null;
    }
    public bool CompareEffect(System.Action a)
    {
        return a.GetMethodInfo().Name == action.GetPersistentMethodName(0);
    }

    public bool IsDisposable()
    {
        return isDisposable;
    }

    public string GetDescription()
    {
        return description;
    }

    public Item Copy()
    {
        Item item = Instantiate(this);
        item.name = this.name;
        return item;
    }
}
