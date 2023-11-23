using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "AbilityLibrary", fileName = "new AbilityLibrary")]
public class AbilityLibrary : ScriptableObject
{
    public void WarriorAbility()
    {
        foreach (var e in FightController.instance.currentFight.enemies)
            e?.TakeDamage(3);
    }

    public void MageAbility()
    {
        FightController.instance.currentFight.enemies.OrderBy(c => c.health).FirstOrDefault()?.TakeDamage(7);
    }

    public void PriestAbility()
    {
        FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderBy(c => c.health).FirstOrDefault().Heal(8);
    }

    public void RogueAbility()
    {
        FightController.instance.currentFight.characters.Where(c => c.ability.CompareEffect(RogueAbility)).FirstOrDefault().usableItem?.Use(2);
    }
    
    public void WarlockAbility()
    {
        Character owner = FightController.instance.currentFight.characters.Where(c => c.ability.CompareEffect(WarlockAbility)).FirstOrDefault();
        int damage = owner.health > 5 ? 5 : Mathf.RoundToInt(owner.health) - 1;
        owner.TakeDamage(damage);
        foreach (var e in FightController.instance.currentFight.enemies)
            e?.TakeDamage(5);
    }

    public void SamuraiAbility()
    {
        FightController.instance.currentFight.enemies.OrderByDescending(c => c.health).FirstOrDefault()?.TakeDamage(9);
    }

    public void DebugAbility()
    {
        Debug.Log("In Battle: ");
        foreach (var c in FightController.instance.currentFight.characters)
            Debug.Log(c.name);
    }

    public void TestHealAbility()
    {
        FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderBy(c => c.health).FirstOrDefault().Heal(8);
    }

    public void TestDamageAbility()
    {
        FightController.instance.currentFight.enemies.OrderBy(c => c.health).FirstOrDefault()?.TakeDamage(7);
    }

    public void TestDamageAllAbility() 
    {
        foreach (var e in FightController.instance.currentFight.enemies)
            e?.TakeDamage(3);
    }
}
