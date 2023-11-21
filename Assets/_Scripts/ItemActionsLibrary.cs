using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemLibrary", fileName = "new ItemLibrary")]
public class ItemActionsLibrary : ScriptableObject
{
    public void DebugItem()
    {
        Debug.Log("!!!");
    }

    public void TestDamageItem()
    {
        if (FightController.instance.currentFight.enemies.Length < 1)
            return;
        FightController.instance.currentFight.enemies[Random.Range(0, FightController.instance.currentFight.enemies.Length)]?.TakeDamage(3);
    }

    public void TestHealAllItem()
    {
        foreach (var c in FightController.instance.currentFight.characters)
            c?.Heal(1);
    }
    public void TestDamageAllItem()
    {
        if (FightController.instance.currentFight.enemies.Length < 1)
            return;
        foreach (var e in FightController.instance.currentFight.enemies)
            e?.TakeDamage(2);
    }

    public void TestEnemyItem()
    {
        FightController.instance.currentFight.characters[Random.Range(0, FightController.instance.currentFight.characters.Length)]?.TakeDamage(2);
    }
}
