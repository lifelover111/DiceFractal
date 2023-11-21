using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Person
{
    protected override void Die()
    {
        FightController.instance.currentFight.RemoveEnemy(this);
        base.Die();
    }
}
