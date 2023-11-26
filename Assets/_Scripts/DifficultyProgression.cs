using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DifficultyProgression
{
    int fightNumber = 1;
    int prevEnemiesTotal = 1;
    int enemyCountM = 2;

    int earlyGameBorder = 3;
    int midGameBorder = 6;
    int lateGameBorder = 9;

    int itemsDropped = 0;
    int itemDropFrequency = 3;
    int currentEnemiesNum;

    List<Item> itemsAlreadyDropped = new List<Item> ();


    public void IncrementFightNumber()
    {
        fightNumber++;
    }

    public Enemy[] GetEnemies() 
    {
        int enemyCount = Random.Range(1, 4);
        if (Random.value > 0.5f)
            if (enemyCount > 1)
            {
                if (Random.value > 1 - ((prevEnemiesTotal / fightNumber) / enemyCountM))
                {
                    enemyCount--;
                }
            }
            else
            {
                if (Random.value > ((prevEnemiesTotal / fightNumber) / enemyCountM))
                {
                    enemyCount++;
                }
            }

        Enemy[] enemies = new Enemy[enemyCount];
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy;
            if(Random.value > 1 - ((fightNumber - midGameBorder) / lateGameBorder))
            {
                enemy = Object.Instantiate(EnemyPrefabManager.instance.lateEnemies[Random.Range(0, EnemyPrefabManager.instance.lateEnemies.Length)]);
            }
            else if (Random.value > 1 - ((fightNumber - earlyGameBorder) / midGameBorder))
            {
                enemy = Object.Instantiate(EnemyPrefabManager.instance.midEnemies[Random.Range(0, EnemyPrefabManager.instance.midEnemies.Length)]);
            }
            else
            {
                enemy = Object.Instantiate(EnemyPrefabManager.instance.earlyEnemies[Random.Range(0, EnemyPrefabManager.instance.earlyEnemies.Length)]);
            }
            enemies[i] = enemy.GetComponent<Enemy>();
            enemy.transform.position = new Vector3(5 * (i + 1), 0, 0);
        }

        prevEnemiesTotal += enemies.Length;
        currentEnemiesNum = enemies.Length;
        return enemies;
    }

    public Item[] GetItemsDrop()
    {
        List<Item> items = new List<Item>();

        if (Random.value > 1/currentEnemiesNum)
        {
            if (Random.value > 0.8f* Mathf.Clamp01((itemsDropped + 1)*itemDropFrequency)/fightNumber)
            {
                if (Random.value > 0.5)
                {
                    Item[] possibleItems = ItemManager.instance.weapons.Where(i => !itemsAlreadyDropped.Contains(i)).ToArray();
                    if (possibleItems.Length > 0)
                    {
                        Item i = possibleItems[Random.Range(0, possibleItems.Length)];
                        itemsAlreadyDropped.Add(i);
                        items.Add(i.Copy());
                        itemsDropped++;
                    }
                }
                else
                {
                    Item i = ItemManager.instance.disposableItems[Random.Range(0, ItemManager.instance.disposableItems.Length)];
                    items.Add(i.Copy());
                    itemsDropped++;
                }
            }
            if (Random.value > 0.97f)
                items.Add(ItemManager.instance.cheese.Copy());
            else if (Random.value > 0.93f)
                items.Add(ItemManager.instance.apple.Copy());
            else
                items.Add(ItemManager.instance.commonHeal.Copy());
        }

        return items.ToArray();
    }
}
