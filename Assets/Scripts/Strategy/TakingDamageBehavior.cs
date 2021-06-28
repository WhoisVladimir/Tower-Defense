using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingDamageBehavior : IAttackable
{
    public event GameObjectActionDelegate OnDeath;

    GameObject gameObject;
    public bool IsInitialize { get; set; }
    public int MaxHP { get; private set; }
    int currHP;

    public TakingDamageBehavior(GameObject gameObject, int HP)
    {
        this.gameObject = gameObject;
        MaxHP = HP;
        currHP = HP;
    }

    public void TakeDamage(IDamager damager)
    {
        currHP -= damager.Damage;
        if (currHP <= 0) 
        {
            currHP = MaxHP;
            OnDeath?.Invoke(gameObject);
        } 
    }
}
