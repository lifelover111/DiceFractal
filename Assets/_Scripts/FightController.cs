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
    [SerializeField] Transform pointer;

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
            c.abilityTurnButton.GetComponent<Button>().interactable = false; //.gameObject.SetActive(false);
    }

    public void EndTurnAbility(int dice)
    {
        currentFight.EndTurn(dice);
        endTurnButton.gameObject.SetActive(false);
    }

    IEnumerator TurnCoroutine()
    {
        if (currentFight.enemies.Length > 0)
        {
            foreach (var dice in currentFight.dices.OrderByDescending(dice => dice.Key))
            {
                Material diceMat = dice.Value.parent.gameObject.GetComponent<MeshRenderer>().material;
                diceMat.color = Color.Lerp(diceMat.color, Color.green, 0.5f);
                foreach (var enemy in currentFight.enemies)
                {
                    if (currentFight.characters.Where(c => !c.isDead).Count() > 0)
                        yield return enemy.StartCoroutine(EnemyTurnCoroutine(enemy, dice.Key));
                }
                foreach (var character in currentFight.characters)
                {
                    yield return character.StartCoroutine(PlayerTurnCoroutine(character, dice.Key));
                }
                diceMat.color = Color.white;
            }
            endTurnButton.gameObject.SetActive(true);
            foreach (var c in currentFight.characters)
            {
                if (c.ability.CheckCost(currentFight.dices.Select(d => d.Key).ToArray()))
                {
                    c.abilityTurnButton.GetComponent<Button>().interactable = true;  //.gameObject.SetActive(true);
                }
            }
        }

        else
        {
            currentFight.RemoveDices();
        }
    }


    IEnumerator EnemyTurnCoroutine(Enemy enemy, int dice)
    {
        if (!enemy.isDead)
            if (enemy.usableItem?.CheckPrice(dice) ?? false)
            {
                pointer.gameObject.SetActive(true);
                Vector3 pos = pointer.position;
                pos.x = enemy.transform.position.x;
                pointer.position = pos;
                yield return StartCoroutine(enemy.UseItem(dice));
                yield return new WaitForSeconds(1f);
                pointer.gameObject.SetActive(false);
            }
    }

    IEnumerator PlayerTurnCoroutine(Character character, int dice)
    {
        if (!character.isDead)
            if (character.usableItem?.CheckPrice(dice) ?? false)
            {
                pointer.gameObject.SetActive(true);
                Vector3 pos = pointer.position;
                pos.x = character.transform.position.x;
                pointer.position = pos;
                yield return StartCoroutine(character.UseItem(dice));
                yield return new WaitForSeconds(1f);
                pointer.gameObject.SetActive(false);
            }
    }

    public void EndFight()
    {
        StartCoroutine(EndFightCoroutine());
    }

    IEnumerator EndFightCoroutine()
    {
        pointer.gameObject.SetActive(false);
        StopAllCoroutines();
        yield return new WaitForSeconds(1f);
        foreach (var c in currentFight.characters)
            c.StopAllCoroutines();
    }

    public IEnumerator DropItemsThenGoForward(System.Action action)
    {
        Debug.Log("Item Dropped!");
        yield return null;
        action.Invoke();
    }


}
