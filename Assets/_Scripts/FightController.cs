using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class FightController : MonoBehaviour
{
    public static FightController instance;
    public Fight currentFight;
    [SerializeField] Transform endTurnButton;

    private void Awake()
    {
        instance = this;
    }


    public void DoTurn()
    {
        StartCoroutine(TurnCoroutine());
    }

    public void EndTurn()
    {
        currentFight.EndTurn();
        endTurnButton.gameObject.SetActive(false);
        foreach (var c in currentFight.characters)
            c.abilityTurnButton.gameObject.SetActive(false);
    }

    public void EndTurnAbility(int dice)
    {
        currentFight.EndTurn(dice);
        endTurnButton.gameObject.SetActive(false);
    }

    IEnumerator TurnCoroutine()
    {
        foreach (var dice in currentFight.dices.OrderByDescending(dice => dice.Key))
        {
            foreach (var enemy in currentFight.enemies)
            {
                yield return enemy.StartCoroutine(EnemyTurnCoroutine(enemy, dice.Key));
            }
            foreach (var character in currentFight.characters)
            {
                yield return character.StartCoroutine(PlayerTurnCoroutine(character, dice.Key));
            }
        }
        endTurnButton.gameObject.SetActive(true);
        foreach(var c in currentFight.characters)
        {
            if(c.ability.CheckCost(currentFight.dices.Select(d => d.Key).ToArray()))
            {
                c.abilityTurnButton.gameObject.SetActive(true);
            }
        }
    }

    IEnumerator EnemyTurnCoroutine(Enemy enemy, int dice)
    {
        if (enemy.usableItem?.CheckPrice(dice) ?? false)
        {
            enemy.usableItem.Use(dice);
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator PlayerTurnCoroutine(Character character, int dice)
    {
        if (character.usableItem?.CheckPrice(dice) ?? false)
        {
            character.usableItem.Use(dice);
            yield return new WaitForSeconds(2f);
        }
    }
}
