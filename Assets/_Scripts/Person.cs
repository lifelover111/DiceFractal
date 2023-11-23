using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public abstract class Person : MonoBehaviour, IHealth
{
    [Header("Set in inspector: ")]
    public int totalHealth;
    public Item usableItem;
    protected Animator anim;

    public bool isDead = false;

    public float health { get; private set; }
    public float maxHealth { get; private set; }

    private void Awake()
    {
        maxHealth = totalHealth;
        health = maxHealth;
        anim = GetComponent<Animator>();
    }

    public virtual void TakeDamage(float damage)
    {
        anim.SetBool("Hit", true);
        StartCoroutine(TakeDamageCoroutine(damage));
    }

    public virtual void Heal(float heal)
    {
        health += heal;
        health = health > maxHealth ? maxHealth : health;
    }
    public IEnumerator UseItem(int dice)
    {
        anim.SetBool("Item", true);
        while (anim.GetBool("Item"))
            yield return null;
        usableItem.Use(dice);
    }

    protected virtual void Die()
    {
        isDead = true;
    }

    protected virtual IEnumerator TakeDamageCoroutine(float damage)
    {
        while (anim.GetBool("Hit"))
        {
            yield return null; 
        }
        health -= damage;
        health = health < 0 ? 0 : health;

        if (health == 0)
        {
            anim.SetBool("Die", true);
            Die();
        }
    }
}
