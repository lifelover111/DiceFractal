using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : Person
{
    [SerializeField] new string name;
    private void Start()
    {
        FightController.instance.currentFight.OnFightEnd += () => { Destroy(gameObject); };
    }
    protected override void Die()
    {
        FightController.instance.currentFight.RemoveEnemy(this);
        GameManager.instance.enemiesKilled++;
        base.Die();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        GameManager.instance.damageDealed += Mathf.RoundToInt(damage);
    }
    public override void Heal(float heal)
    {
        base.Heal(heal);
    }

    public void SetIdle()
    {
        anim.SetBool("Item", false);
        anim.SetBool("Hit", false);
        anim.SetBool("Die", false);
    }

    public void Resurrect()
    {
        if (!isDead)
            return;
        isDead = false;
        health = maxHealth;
        SetIdle();
        anim.Play("Idle");
        anim.speed = 1;
    }

    public void DieEvent()
    {
        anim.speed = 0;
        StopAllCoroutines();
    }

    private void OnMouseEnter()
    {
        GameInfo.instance.gameObject.SetActive(true);
        Vector2 infoPosition = Camera.main.WorldToScreenPoint(transform.position) - 650 * (Screen.width / 1920) * Vector3.right;
        GameInfo.instance.SetInfo(infoPosition, name, usableItem.GetDescription());
    }
    private void OnMouseExit()
    {
        GameInfo.instance.gameObject.SetActive(false);
    }
}
