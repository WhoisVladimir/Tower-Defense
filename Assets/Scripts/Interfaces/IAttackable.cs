public interface IAttackable
{
    public event TriggerDelegate OnTriggerAction;
    public event GameObjectActionDelegate OnDeath;
    public int MaxHP { get; }
    public void TakeDamage(IDamager damager);
}
