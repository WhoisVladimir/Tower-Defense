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
            attackable.TakeDamage(other.gameObject.GetComponent<IDamager>());
        } 
    }

    public void InitializeMonster(IAttackable attackable, IMovable movable,
        IReturnable returnable, IDetector detector, int damage = 0)
    {
        this.attackable = attackable;
        this.movable = movable;
        this.returnable = returnable;
        this.detector = detector;
        Damage = damage;
        IsInitialize = true;
        detector?.DetectTarget();
    }
}
