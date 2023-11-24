using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

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
        
        /////////////button.onClick.AddListener(() => { FightController.instance.EndTurnAbility(ability.AbilityCost()); });
        //abilityTurnButton.gameObject.GetComponentInChildren<TMP_Text>().text = "Cost: " + ability.GetStringCost();

        //Ability icon
        UnityEngine.UI.Image image = abilityTurnButton.gameObject.GetComponent<UnityEngine.UI.Image>();
        image.sprite = ability.abilityIcon;

        //Cost dots
        TMP_Text textComponent = abilityTurnButton.gameObject.GetComponentInChildren<TMP_Text>();
        int cost = int.Parse(ability.GetStringCost());
        textComponent.text = GetDiceSymbol(cost);



        // AbilityPointEnterOut
        EventTrigger trigger = abilityTurnButton.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
        pointerEnter.eventID = EventTriggerType.PointerEnter;

        pointerEnter.callback.AddListener((data) => { OnPointerEnter(); });

        trigger.triggers.Add(pointerEnter);

        EventTrigger.Entry pointerExit = new EventTrigger.Entry();
        pointerExit.eventID = EventTriggerType.PointerExit;
        pointerExit.callback.AddListener((data) => { OnPointerExit(); });
        trigger.triggers.Add(pointerExit);

    }

    void OnPointerEnter()
    {
        tooltipScreen.ShowTooltip_Static(ability.GetDescription());
    }


    void OnPointerExit()
    {
        tooltipScreen.HideTooltip_static();
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
    public void Resurrect()
    {
        if (!isDead)
            return;
        SetIdle();
        isDead = false;
        health = maxHealth;
        anim.Play("Idle");
        anim.speed = 1;
    }

    public void DieEvent()
    {
        anim.speed = 0;
        StopAllCoroutines();
    }
}
