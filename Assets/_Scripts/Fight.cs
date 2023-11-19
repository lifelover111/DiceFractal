using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fight
{
    public Character[] characters;
    public Enemy[] enemies;
    public List<Dice> dices = new List<Dice>();
    FightController fightController;

    public Fight(Character[] characters, int startDices, int enemiesNum = 0)
    {
        fightController = FightController.instance;
        this.characters = characters;
        if (enemiesNum == 0)
            enemies = new Enemy[Random.Range(0, 4)];
        else
            enemies = new Enemy[enemiesNum];
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = SpawnEnemy();
        }

        for(int i = 0; i < startDices; i++) 
        {
            dices.Add(new Dice());
        }
    }

    Enemy SpawnEnemy()
    {
        ///
        Enemy enemy = new Enemy();
        return enemy;
    }

    void StartTurn()
    {
        fightController.DoTurn(this);
    }

    public void EndTurn()
    {
        int dicesNumNext = dices.Min(dice => dice.value);
        dices.Clear();
        for(int i = 0; i < dicesNumNext; i++)
        {
            dices.Add(new Dice());
        }
        StartTurn();
    }
}
