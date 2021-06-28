using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour, IDamager
{
    IAttackable attackable;
    IMovable movable;
    IReturnable returnable;
    IDetector detector;

    public bool IsInitialize { get; private set; }
    public int Damage { get; private set; }

    void Awake()
    {
        gameObject.tag = "Enemy";
    }
    private void FixedUpdate()
    {
        movable?.Move();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile")) 
        {
            Debug.Log($"{gameObject.name} {gameObject.GetInstanceID()} take damage from {other.gameObject.name}.");
            attackable.TakeDamage(other.gameObject.GetComponent<IDamager>());
        } 
    }

    public void InitializeMonster(IAttackable attackable, IMovable movable,
        IReturnable returnable, IDetector detector, int damage = 0)
    {
        if (attackable != null) this.attackable = attackable;
        if (movable != null) this.movable = movable;
        if (returnable != null) this.returnable = returnable;
        if (detector != null) this.detector = detector;
        Damage = damage;
        IsInitialize = true;
        detector?.DetectTarget();
    }
}
