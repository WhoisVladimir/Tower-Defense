using UnityEngine;

public abstract class TowerController
{
    // Общий абстрактный класс поведения башен.

    protected GameObject projectile;
    protected GameObject tower;
    protected GameObject projectileRespawn;

    protected ObjectPool projPool;

    public TowerController(GameObject projectile, GameObject tower)
    {
        this.projectile = projectile;
        this.tower = tower;
    }
    public virtual void Start()
    {
        InstantiateWeaponSystem();
        InitializeSystem();
    }

    protected virtual void InstantiateWeaponSystem()
    {
        tower = GameObject.Instantiate(tower);
        projectileRespawn = tower.transform.GetChild(0).gameObject;
        projectile.transform.position = projectileRespawn.transform.position;
    }

    protected abstract void InitializeSystem();
}
