using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyProgression
{
    int fightNumber = 1;
    int prevEnemiesTotal = 1;
    int enemyCountM = 2;

    int earlyGameBorder = 3;
    int midGameBorder = 6;
    int lateGameBorder = 9;


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
        return enemies;
    }

    Item[] GetItemsDrop()
    {
        return new Item[0];
    }
}
