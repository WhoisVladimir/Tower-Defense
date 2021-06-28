using UnityEngine;
public interface IAttackable
{
    public event GameObjectActionDelegate OnDeath;
    public int MaxHP { get; }
    public void TakeDamage(IDamager damager);
}
