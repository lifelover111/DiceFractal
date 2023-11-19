using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FightController : MonoBehaviour
{
    public static FightController instance;
    Fight currentFight;
    [SerializeField] Transform endTurnButton;
    private void Awake()
    {
        instance = this;
    }

    void StartNewFight()
    {
        //Fight fight = new Fight()
        /*
        Button button = endTurnButton.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { fight.EndTurn(); });
        */
    }

    public void DoTurn(Fight fight)
    {
        currentFight = fight;
        StartCoroutine(TurnCoroutine());
    }

    IEnumerator TurnCoroutine()
    {
        foreach(var dice in currentFight.dices)
        {
            foreach(var enemy in currentFight.enemies)
            {
                yield return enemy.StartCoroutine(EnemyTurnCoroutine(enemy));
            }
        }
        foreach (var dice in currentFight.dices)
        {
            foreach (var character in currentFight.characters)
            {
                yield return character.StartCoroutine(PlayerTurnCoroutine(character));
            }
        }
        endTurnButton.gameObject.SetActive(true);
    }

    IEnumerator EnemyTurnCoroutine(Enemy enemy)
    {
        yield return new WaitForSeconds(2.0f);
    }

    IEnumerator PlayerTurnCoroutine(Character character)
    {
        yield return new WaitForSeconds(2.0f);
    }
}
