using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Person
{
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


    public void DieEvent()
    {
        anim.speed = 0;
        StopAllCoroutines();
    }
}
