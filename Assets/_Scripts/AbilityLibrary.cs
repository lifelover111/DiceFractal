using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "AbilityLibrary", fileName = "new AbilityLibrary")]
public class AbilityLibrary : ScriptableObject
{
    public void DebugAbility()
    {
        Debug.Log("In Battle: ");
        foreach (var c in FightController.instance.currentFight.characters)
            Debug.Log(c.name);
    }

    public void TestHealAbility()
    {
        FightController.instance.currentFight.characters.OrderBy(c => c.health).FirstOrDefault().Heal(6);
    }

    public void TestDamageAbility()
    {
        FightController.instance.currentFight.enemies.OrderBy(c => c.health).FirstOrDefault().TakeDamage(5);
    }

    public void TestDamageAllAbility() 
    {
        foreach (var e in FightController.instance.currentFight.enemies)
            e.TakeDamage(2);
    }
}
