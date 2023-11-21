using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            /// ��� �� ���������, ���� � ������ ��������� ���������� �������
            return suitableDices[0];
        }

    }

    [SerializeField] Cost price;
    [SerializeField] UnityEvent OnUse;

    public bool CheckPrice(int value)
    {
        return price.Contains(value);
    }
    public void Use(int dice)
    {
        OnUse?.Invoke();
    }
}
