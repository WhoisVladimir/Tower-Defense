using UnityEngine;

public abstract class TowerController
{
    // Общий абстрактный класс поведения башен.

    protected GameObject projectile;
    protected GameObject tower;
    protected GameObject projectileRespawn;

    protected ObjectPool projPool;

    public TowerController(GameObject projectile, GameObject tower, GameObject projectileRespawn)
    {
        this.projectile = projectile;
        this.tower = tower;
        this.projectileRespawn = projectileRespawn;
    }
    public virtual void Start()
    {
        InstantiateWeaponSystem();
        InitializeSystem();
    }

    protected virtual void InstantiateWeaponSystem()
    {
        tower = Object.Instantiate(tower);
        projectileRespawn = Object.Instantiate(projectileRespawn);
        projectile.transform.position = projectileRespawn.transform.position;
    }

    protected abstract void InitializeSystem();
}
