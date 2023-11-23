using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Character : Person
{
    public Ability ability;
    public Transform abilityTurnButton;
    public event System.Action OnAbilityUse = () => { };
    public event System.Action OnCharacterDied = () => { };

    private void Start()
    {
        UnityEngine.UI.Button button = abilityTurnButton.gameObject.GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(UseAbility);
        OnAbilityUse += () => { FightController.instance.EndTurnAbility(ability.AbilityCost()); };
        abilityTurnButton.gameObject.GetComponentInChildren<TMP_Text>().text = "Cost: " + ability?.GetStringCost();
    }
    

    public void UseAbility()
    {
        anim.SetBool("Ability", true);
        StartCoroutine(UseAbilityCoroutine());
    }

    IEnumerator UseAbilityCoroutine()
    {
        while (anim.GetBool("Ability"))
            yield return null;
        ability.Use();
        OnAbilityUse?.Invoke();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        GameManager.instance.damageTaken += Mathf.RoundToInt(damage);
    }
    public override void Heal(float heal)
    {
        base.Heal(heal);
        GameManager.instance.healthRestored += Mathf.RoundToInt(heal);
    }

    public void SetIdle()
    {
        anim.SetBool("Ability", false);
        anim.SetBool("Item", false);
        anim.SetBool("Hit", false);
        anim.SetBool("Move", false);
        anim.SetBool("Die", false);
    }

    public void GoForward()
    {
        anim.SetBool("Move", true);
    }

    protected override void Die()
    {
        base.Die();
        OnCharacterDied?.Invoke();
    }

    public void DieEvent()
    {
        anim.speed = 0;
        StopAllCoroutines();
    }
}
