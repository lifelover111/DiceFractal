using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemLibrary", fileName = "new ItemLibrary")]
public class ItemActionsLibrary : ScriptableObject
{
    //Стартовое оружие персонажей
    public void CommonSword()
    {
        if (FightController.instance.currentFight.enemies.Length < 1)
            return;
        FightController.instance.currentFight.enemies[Random.Range(0, FightController.instance.currentFight.enemies.Length)]?.TakeDamage(3);
    }

    public void PrayerBook()
    {
        foreach (var c in FightController.instance.currentFight.characters)
            if(!c.isDead)
                c.Heal(1);
    }
    public void FireStaff()
    {
        if (FightController.instance.currentFight.enemies.Length < 1)
            return;
        foreach (var e in FightController.instance.currentFight.enemies)
            e?.TakeDamage(2);
    }
    public void ShortBow()
    {
        FightController.instance.currentFight.enemies.OrderBy(e => e.health).FirstOrDefault()?.TakeDamage(5);
    }
    public void OldKatana()
    {
        Enemy[] enemies = FightController.instance.currentFight.enemies.OrderBy(e => Random.value).ToArray();
        for (int i = 0; i < (enemies.Length > 2 ? 2 : enemies.Length); i++)
            enemies[i].TakeDamage(2);
    }
    public void SpellfullSkull()
    {
        Enemy[] enemies = FightController.instance.currentFight.enemies.OrderBy(e => Random.value).ToArray();
        Character owner = FightController.instance.currentFight.characters.Where(c => c.usableItem.CompareEffect(SpellfullSkull)).FirstOrDefault();
        for (int i = 0; i < (enemies.Length > 2 ? 2 : enemies.Length); i++)
        { 
            enemies[i].TakeDamage(1);
            owner.Heal(1);
        }
    }

    // Доп оружие

    // Оружие противников
    public void SkeletonItem()
    {
        FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderBy(c => Random.value).FirstOrDefault().TakeDamage(3);
    }
}
