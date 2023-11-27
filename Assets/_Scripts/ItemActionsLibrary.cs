using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
        FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderBy(c => c.health).FirstOrDefault().Heal(1);
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
        Character owner = FightController.instance.currentFight.characters.Where(c => c.usableItem?.CompareEffect(SpellfullSkull) ?? false).FirstOrDefault();
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
        foreach(var c in characters)
            c.Resurrect();
    }

    public void Apple()
    {
        foreach(var c in FightController.instance.currentFight.characters.Where(x => !x.isDead))
        {
            c.Heal(Mathf.RoundToInt(c.maxHealth / 2));
        }
    }

    public void HealingPotion()
    {
        Character character = FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderBy(c => c.health).FirstOrDefault();
        character.Heal(Mathf.RoundToInt(character.maxHealth/2));
    }

    public void Bomb()
    {
        foreach(Enemy e in FightController.instance.currentFight.enemies)
        {
            e.TakeDamage(10);
        }
    }
    public void HolyArrow()
    {
        Enemy target = FightController.instance.currentFight.enemies.OrderByDescending(e => e.health).FirstOrDefault();
        target.TakeDamage(target.health);
    }


    public void LongSword()
    {
        if (FightController.instance.currentFight.enemies.Length < 1)
            return;
        FightController.instance.currentFight.enemies[Random.Range(0, FightController.instance.currentFight.enemies.Length)]?.TakeDamage(5);
    }

    public void VampireSword()
    {
        if (FightController.instance.currentFight.enemies.Length < 1)
            return;
        Enemy target = FightController.instance.currentFight.enemies[Random.Range(0, FightController.instance.currentFight.enemies.Length)];
        float damage = target.health > 5 ? 5 : target.health;
        target.TakeDamage(damage);
        FightController.instance.currentFight.characters.Where(c => c.usableItem.CompareEffect(VampireSword)).FirstOrDefault().Heal(5);
    }

    public void LongBow()
    {
        FightController.instance.currentFight.enemies.OrderBy(e => e.health).FirstOrDefault()?.TakeDamage(7);
    }

    public void CompositeBow()
    {
        int targets = FightController.instance.currentFight.enemies.Length >= 2 ? 2 : FightController.instance.currentFight.enemies.Length;
        for (int i = 0; i < targets; i++)
        {
            FightController.instance.currentFight.enemies.OrderBy(e => e.health).ToArray()[i].TakeDamage(5);
        }
    }

    public void WitheringStaff()
    {
        if (FightController.instance.currentFight.enemies.Length < 1)
            return;
        Character owner = FightController.instance.currentFight.characters.Where(c => c.usableItem.CompareEffect(WitheringStaff)).FirstOrDefault();
        foreach (var e in FightController.instance.currentFight.enemies)
        { 
            e?.TakeDamage(2);
            owner.Heal(1);
        }
    }

    public void AssassinDagger()
    {
        FightController.instance.currentFight.enemies.OrderBy(e => e.health).FirstOrDefault()?.TakeDamage(4);
    }

    public void CursedUchigatana()
    {
        Enemy target = FightController.instance.currentFight.enemies.OrderByDescending(e => e.health).FirstOrDefault();
        float targetHealth = target.health;
        target.TakeDamage(8);
        if(targetHealth <= 8)
        {
            FightController.instance.currentFight.characters.Where(c => c.usableItem.CompareEffect(CursedUchigatana)).FirstOrDefault().Heal(5);
        }
    }

    public void HealPholiant()
    {
        foreach (Character ch in FightController.instance.currentFight.characters)
            ch.Heal(3);
        foreach (Enemy e in FightController.instance.currentFight.enemies)
            e.Heal(3);
    }

    public void MagicalStaff()
    {
        float sumHealth = 0;
        foreach(Enemy enemy in FightController.instance.currentFight.enemies)
        {
            sumHealth += enemy.health;
        }
        int c = Mathf.RoundToInt(sumHealth) / FightController.instance.currentFight.enemies.Length;
        int o = Mathf.RoundToInt(sumHealth) - c;

        foreach (Enemy enemy in FightController.instance.currentFight.enemies)
        {
            if (enemy.health > c)
                enemy.TakeDamage(enemy.health - (c - (o > 0 ? 1 : 0)));
            else
                enemy.Heal((c + (o > 0 ? 1 : 0)) - enemy.health);
            if (enemy.health != c)
                o--;

            enemy.TakeDamage(2);
        }
    }

    public void ButcherKnife()
    {
        FightController.instance.currentFight.enemies[Random.Range(0, FightController.instance.currentFight.enemies.Length)]?.TakeDamage(8);
    }

    public void ArtoriasSword()
    {
        Enemy[] enemies = FightController.instance.currentFight.enemies.OrderBy(e => Random.value).ToArray();
        for(int i = 0; i < (enemies.Length < 2 ? enemies.Length : 2); i++)
        {
            enemies[i].TakeDamage(6);
        }
    }
    public void HunterKnife()
    {
        FightController.instance.currentFight.enemies.OrderByDescending(e => e.health).FirstOrDefault()?.TakeDamage(5);
    }

    public void AxeOfMadness()
    {
        int enemyCount = FightController.instance.currentFight.enemies.Length;
        int target = Random.Range(0, enemyCount);
        FightController.instance.currentFight.enemies[target].TakeDamage(7);
        if (enemyCount < 2)
            return;
        int secondTarget = target + 1 > enemyCount - 1 ? target - 1 : target + 1;
        FightController.instance.currentFight.enemies[secondTarget].TakeDamage(3);
    }

    // Оружие противников
    public void SkeletonItem()
    {
        FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderBy(c => Random.value).FirstOrDefault().TakeDamage(2);
    }
    public void ObserverItem()
    {
        Character[] characters = FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderBy(c => Random.value).ToArray();
        for (int i = 0; i < (characters.Length > 2 ? 2 : characters.Length); i++)
            characters[characters.Length - 1 - i].TakeDamage(1);
    }
    public void GoblinItem()
    {
        FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderBy(c => c.health).FirstOrDefault().TakeDamage(1);
    }
    public void MushroomItem()
    {
        FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderByDescending(c => c.health).FirstOrDefault().TakeDamage(5);
    }
    public void BringerOfDeathItem()
    {
        foreach (var c in FightController.instance.currentFight.characters.Where(ch => !ch.isDead))
            c?.TakeDamage(3);
    }
    public void PikemanItem()
    {
        Character[] characters = FightController.instance.currentFight.characters.Where(c => !c.isDead).ToArray();
        if(characters.Length >= 2)
        {
            int target = Random.Range(1, characters.Length);
            characters[target].TakeDamage(4);
            characters[target-1].TakeDamage(2);
        }
        else
            characters[0].TakeDamage(4);
    }
    public void BanditItem()
    {
        Character[] characters = FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderBy(c => Random.value).ToArray();
        for (int i = 0; i < (characters.Length > 2 ? 2 : characters.Length); i++)
            characters[characters.Length - 1 - i].TakeDamage(3);
    }

    public void SlimeItem()
    {
        Character[] characters = FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderBy(c => Random.value).ToArray();
        characters[Random.Range(0, characters.Length)].TakeDamage(4);
        Enemy[] slimes = FightController.instance.currentFight.enemies.Where(e => e.usableItem.CompareEffect(SlimeItem)).ToArray();
        foreach (Enemy s in slimes) 
        {
            s.Heal(2);
        }
    }

    public void ShadowItem()
    {
        FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderByDescending(c => c.health).FirstOrDefault().TakeDamage(7);
    }

    public void GuardItem()
    {
        Character[] characters = FightController.instance.currentFight.characters.Where(c => !c.isDead).OrderByDescending(c => c.health).ToArray();
        for (int i = 0; i < characters.Length; i++)
            characters[i].TakeDamage(4 - i);
    }
}
