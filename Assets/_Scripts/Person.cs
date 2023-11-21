using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public abstract class Person : MonoBehaviour, IHealth
{
    [Header("Set in inspector: ")]
    public int totalHealth;
    public Item usableItem;

    public float health { get; private set; }
    public float maxHealth { get; private set; }

    private void Awake()
    {
        maxHealth = totalHealth;
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = health < 0 ? 0 : health;
        if (health == 0)
            Die();
    }

    public void Heal(float heal)
    {
        health += heal;
        health = health > maxHealth ? maxHealth : health;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
