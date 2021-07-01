using UnityEngine;

public class Projectile : MonoBehaviour, IDamager
{
    public IMovable Movable { get; private set; }
    IReturnable returnable;
    public int Damage { get; private set; }
    public bool IsInitialize { get; private set; }
    private void Awake()
    {
        gameObject.tag = "Projectile";
    }
    private void FixedUpdate()
    {
        Movable.Move();
    }
    public void InitializeProjectile(IMovable movable, IReturnable returnable, int damage = 0)
    {
        this.Movable = movable;
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
