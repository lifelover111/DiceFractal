using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : Person
{
    public Ability ability;
    public Transform abilityTurnButton;
    public event System.Action OnAbilityUse = () => { };

    private void Start()
    {
        UnityEngine.UI.Button button = abilityTurnButton.gameObject.GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(UseAbility);
        button.onClick.AddListener(() => { FightController.instance.EndTurnAbility(ability.AbilityCost()); });
        //abilityTurnButton.gameObject.GetComponentInChildren<TMP_Text>().text = "Cost: " + ability.GetStringCost();

        UnityEngine.UI.Image image = abilityTurnButton.gameObject.GetComponent<UnityEngine.UI.Image>();
        image.sprite = ability.abilityIcon;

        TMP_Text textComponent = abilityTurnButton.gameObject.GetComponentInChildren<TMP_Text>();

        int cost = int.Parse(ability.GetStringCost());
        textComponent.text = GetDiceSymbol(cost);

    }

    private string GetDiceSymbol(int count)
    {
        switch (count)
        {
            case 1:
                return "●";
            case 2:
                return "● ●";
            case 3:
                return "● ● ●";
            case 4:
                return "● ● \n● ●";
            case 5:
                return "● ●\n● ● ●";
            case 6:
                return "● ● ●\n● ● ●";
            default:
                return ""; 
        }
    }

    public void UseAbility()
    {
        OnAbilityUse?.Invoke();
        ability.Use();
    }
}
