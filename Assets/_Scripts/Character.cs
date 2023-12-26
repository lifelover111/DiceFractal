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
    public Transform equippedItemSlot;
    public event System.Action OnAbilityUse = () => { };
    public event System.Action OnCharacterDied = () => { };

    public AudioSource abilityAudioSource;

    private void Start()
    {
        UnityEngine.UI.Button button = abilityTurnButton.gameObject.GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(UseAbility);
        OnAbilityUse += () => { FightController.instance.EndTurnAbility(ability.AbilityCost()); };

        abilityTurnButton.gameObject.GetComponent<AbilityButton>().SetAbility(ability);

        GameObject itemInventory = Instantiate(GameManager.instance.inventoryItemPrefab);
        itemInventory.transform.SetParent(equippedItemSlot);
        RectTransform itemRect = itemInventory.GetComponent<RectTransform>();
        itemRect.anchoredPosition3D = Vector3.zero;
        itemRect.localScale = Vector2.one;

        itemInventory.GetComponent<ItemInventory>().SetItem(usableItem);
        ItemSlot itemSlot = equippedItemSlot.GetComponent<ItemSlot>();
        itemSlot.OnSlotDrop += () => { usableItem = equippedItemSlot.GetChild(0).gameObject.GetComponent<ItemInventory>().GetItem(); };
        itemSlot.OnSlotEmpty += () => { usableItem = null; };
    }

    public void UseAbility()
    {
        FightController.instance.playerTurn = false;
        anim.SetBool("Ability", true);
        if (abilityAudioSource != null)
        {
            abilityAudioSource.Play();
        }
        StartCoroutine(UseAbilityCoroutine());
    }

    IEnumerator UseAbilityCoroutine()
    {
        while (anim.GetBool("Ability"))
            yield return null;
        ability.Use();
     
        OnAbilityUse?.Invoke();
    }

    //public override IEnumerator UseItem(int dice)
    //{
    //    if (abilityAudioSource != null)
    //    {
    //        abilityAudioSource.Play();
    //    }
    //    base.UseItem(dice);
    //    yield return null;
    //}

    public override IEnumerator UseItem(int dice)
    {
        if (abilityAudioSource != null)
        {
            abilityAudioSource.Play();
        }
        anim.SetBool("Item", true);

        while (anim.GetBool("Item"))
            yield return null;
        usableItem.Use(dice);
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
