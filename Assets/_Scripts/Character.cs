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
        abilityTurnButton.gameObject.GetComponentInChildren<TMP_Text>().text = "Cost: " + ability.GetStringCost();
    }

    public void UseAbility()
    {
        OnAbilityUse?.Invoke();
        ability.Use();
    }
}
