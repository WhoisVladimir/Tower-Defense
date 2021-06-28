using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IDamager
{
    IMovable movable;
    IReturnable returnable;
    public int Damage { get; private set; }
    public bool IsInitialize { get; private set; }
    private void Awake()
    {
        gameObject.tag = "Projectile";
    }
    private void FixedUpdate()
    {
        movable.Move();
    }
    public void InitializeProjectile(IMovable movable, IReturnable returnable, int damage = 0)
    {
        this.movable = movable;
        this.returnable = returnable;
        Damage = damage;
        IsInitialize = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Respawn")) returnable.ResetPosition(gameObject);
        if (other.gameObject.CompareTag("Enemy")) 
        {
            gameObject.SetActive(false);
        } 
    }

}
