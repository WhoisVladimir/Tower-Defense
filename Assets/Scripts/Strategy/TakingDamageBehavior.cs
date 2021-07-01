using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingDamageBehavior : IAttackable
{
    //Стратегия получения урона. Реализует простое вхождение урона.

    public event GameObjectActionDelegate OnDeath;
    public event TriggerDelegate OnTriggerAction;

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
        // Вхождение урона.
        currHP -= damager.Damage;
        if (currHP <= 0) 
        {
            currHP = MaxHP;
            OnDeath?.Invoke(gameObject);
        } 
    }
}
