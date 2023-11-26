using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fight
{
    public Character[] characters;
    public Enemy[] enemies;
    public List<KeyValuePair<int, Transform>> dices = new List<KeyValuePair<int, Transform>>();
    FightController fightController;
    int requiredDiceNum;
    public event System.Action OnFightEnd = () => { };

    DifficultyProgression progression;

    public Fight(Character[] characters, DifficultyProgression progression)
    {
        this.progression = progression;
        fightController = FightController.instance;
        this.characters = characters;
        PlaceCharacters();
        enemies = progression.GetEnemies();
        fightController.currentFight = this;
    }

    public void StartFight()
    {
        GameObject dice = Object.Instantiate(GameManager.instance.dicePrefab);
        dice.transform.position = GameManager.instance.dicesPosition;
        DiceThrow diceThrow = dice.GetComponent<DiceThrow>();
        diceThrow.OnValueGot += () =>
        {
            requiredDiceNum = diceThrow.GetValue().Key;
            Zoom zoom = dice.GetComponent<Zoom>();
            zoom.enabled = true;
            zoom.OnZoom += () =>
            {
                for (int i = 0; i < diceThrow.GetValue().Key; i++)
                {
                    RollDice(diceThrow.GetValue().Value.GetChild(i));
                }
            };
        };
        diceThrow.ThrowDice();
    }

    void RollDice(Transform targetTransform) 
    {
        GameObject dice = Object.Instantiate(GameManager.instance.dicePrefab);
        dice.transform.position = GameManager.instance.dicesPosition;
        dice.transform.position += targetTransform.rotation * targetTransform.localPosition * 8;
        Vector3 dicePos = dice.transform.position;
        dicePos.z = -0.75f;
        dice.transform.position = dicePos;
        DiceThrow diceThrow = dice.GetComponent<DiceThrow>();
        diceThrow.OnValueGot += () =>
        {
            dices.Add(diceThrow.GetValue());
            if (dices.Count == requiredDiceNum)
                StartTurn();
        };
        diceThrow.ThrowDice();
    }

    void PlaceCharacters()
    {
        int i = 3;
        foreach(var c in characters)
        {
            c.transform.position = new Vector3(-5 * i, 0, 0);
            i--;
        }
    }


    public void RemoveEnemy(Enemy enemy)
    {
        Enemy[] newEnemies = new Enemy[enemies.Length - 1];
        int i = 0;
        foreach(var e in enemies)
        {
            if(e != enemy)
            {
                newEnemies[i] = e;
                i++;
            }
        }
        enemies = newEnemies;
        if (enemies.Length == 0)
            EndFight();
    }
    void EndFight()
    {
        fightController.EndFight();
        RemoveDices();
        Item[] itemsDropped = progression.GetItemsDrop();
        if (itemsDropped.Length > 0)
        {
            foreach (var item in itemsDropped)
            {
                Inventory.instance.AddItem(item);
            }
            fightController.StartCoroutine(fightController.DropItemsThenGoForward(OnFightEnd.Invoke));
        }
        else
            OnFightEnd?.Invoke();
    }

    public void StartTurn()
    {
        fightController.DoTurn();
    }

    public void EndTurn()
    {
        KeyValuePair<int, Transform> diceNext = dices.OrderBy(dice => dice.Key).FirstOrDefault();
        RemoveDices(diceNext);
        Zoom zoom = diceNext.Value.parent.gameObject.GetComponent<Zoom>();
        zoom.enabled = true;
        zoom.OnZoom += () =>
        {
            Object.Destroy(diceNext.Value.parent.gameObject);
            requiredDiceNum = diceNext.Key;
            for (int i = 0; i < diceNext.Key; i++)
            {
                RollDice(diceNext.Value.GetChild(i));
            }
        };
    }
    
    public void EndTurn(int dice)
    {
        KeyValuePair<int, Transform> diceNext = dices.Where(d => d.Key == dice).FirstOrDefault();
        RemoveDices(diceNext);
        Zoom zoom = diceNext.Value.parent.gameObject.GetComponent<Zoom>();
        zoom.enabled = true;
        zoom.OnZoom += () =>
        {
            Object.Destroy(diceNext.Value.parent.gameObject);
            requiredDiceNum = diceNext.Key;
            for (int i = 0; i < diceNext.Key; i++)
            {
                RollDice(diceNext.Value.GetChild(i));
            }
        };
    }


    public void RemoveDices(KeyValuePair<int, Transform>? except = null)
    {
        if(except != null)
        {
            dices.Remove(except.Value);
        }
        foreach(var d in dices)
        {
            Object.Destroy(d.Value.parent.gameObject);
        }
        dices.Clear();
    }
}
