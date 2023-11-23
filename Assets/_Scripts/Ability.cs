using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Ability", fileName = "new Ability")]
public class Ability : ScriptableObject
{

    public Sprite abilityIcon;

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

    [SerializeField] UnityEvent OnUse;
    [SerializeField] Cost cost;

    [TextArea(3, 10)]
    [SerializeField] string description;

    public void Use()
    {
        OnUse?.Invoke();
    }

    public int AbilityCost()
    {
        return cost.AbilityCost();
    }
    public bool CheckCost(int[] dices)
    {
        return cost.CheckCost(dices);
    }
    public int GetDice(int[] dices)
    {
        return dices.Where(dice => cost.Contains(dice)).Min();
    }
    public string GetStringCost() 
    {
        return cost.GetStringCost();
    }


    public string GetDescription()
    {
        return description;
    }
}

