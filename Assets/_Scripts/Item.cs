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
        public bool CheckCost(int[] dices)
        {
            bool contains = false;
            foreach (int d in suitableDices)
            {
                if (dices.Contains(d))
                    contains = true;
            }
            return contains;
        }
        public bool Contains(int val)
        {
            return suitableDices.Contains(val);
        }
        public string GetStringCost()
        {
            return string.Join("/", suitableDices);
        }

        public int AbilityCost()
        {
            /// Тут же сломается, если у абилки несколько подходящих бросков
            return suitableDices[0];
        }

    }

    [SerializeField] Cost price;
    [SerializeField] UnityEvent action;
    public event System.Action OnUse = () => { };

    public bool CheckPrice(int value)
    {
        return price.Contains(value);
    }
    public void Use(int dice)
    {
        OnUse?.Invoke();
        action?.Invoke();
    }
    public bool CompareEffect(System.Action a)
    {
        return a.GetMethodInfo().Name == action.GetPersistentMethodName(0);
    }
}
