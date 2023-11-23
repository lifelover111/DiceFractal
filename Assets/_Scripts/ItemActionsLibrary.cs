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
            enemies[enemies.Length - 1 - i].TakeDamage(2);
    }
    public void SpellfullSkull()
    {
        Enemy[] enemies = FightController.instance.currentFight.enemies.OrderBy(e => Random.value).ToArray();
        Character owner = FightController.instance.currentFight.characters.Where(c => c.usableItem.CompareEffect(SpellfullSkull)).FirstOrDefault();
        for (int i = 0; i < (enemies.Length > 2 ? 2 : enemies.Length); i++)
        { 
            enemies[enemies.Length - 1 - i].TakeDamage(1);
            owner.Heal(1);
        }
    }

    // Доп предметы
    public void Cheese()
    {
        Character[] characters = FightController.instance.currentFight.characters.Where(c => c.isDead).ToArray();
        if (characters.Length == 0)
            return;
        characters[Random.Range(0, characters.Length)].Resurrect();
    }

    // Оружие противников
    public void SkeletonItem()
    {
        FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderBy(c => Random.value).FirstOrDefault().TakeDamage(3);
    }
    public void ObserverItem()
    {
        Character[] characters = FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderBy(c => Random.value).ToArray();
        for (int i = 0; i < (characters.Length > 2 ? 2 : characters.Length); i++)
            characters[characters.Length - 1 - i].TakeDamage(2);
    }
    public void GoblinItem()
    {
        FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderBy(c => c.health).FirstOrDefault().TakeDamage(2);
    }
    public void MushroomItem()
    {
        FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderByDescending(c => c.health).FirstOrDefault().TakeDamage(4);
    }
    public void BringerOfDeathItem()
    {
        foreach (var c in FightController.instance.currentFight.characters.Where(ch => !ch.isDead))
            c?.TakeDamage(3);
    }
}
