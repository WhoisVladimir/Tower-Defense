using UnityEngine;

public class CannonTowerController : TowerController
{
    // Класс реализующий поведение башни, стреляющей на упреждение.

    IDetector towDetection;
    IReturnable resetPosition;
    IMovable movable;
    IShooterable shooterable;
    int projectileDamage = 10;

    public CannonTowerController(GameObject projectile, GameObject tower, GameObject projectileRespawn)
        : base(projectile, tower, projectileRespawn)
    {
    }
  
    protected override void InitializeSystem()
    {
        towDetection = new NearestMotionDetectBehavior(tower);
        shooterable = new PreemptiveShootBehavior(projectileRespawn, towDetection);

        SmartTower towLogic = tower.GetComponent<SmartTower>();
        towLogic.InitializeTower(towDetection, shooterable, null);
        shooterable.OnTriggerAction += Shooterable_OnTriggerAction;
        shooterable.OnShot += Shooterable_OnShot;

        projPool = new ObjectPool(projectile);
    }

    private void Shooterable_OnTriggerAction()
    {
        // Активация снаряда, для получения информации о нём.
        ActivateProjectile();
        shooterable.GetProjectileData(movable);
    }

    private void Shooterable_OnShot(GameObject sender, GameObject target)
    {
        // Активация снаряда для запуска.
        ActivateProjectile();
        movable.PointToTarget(target);
        projectile.SetActive(true);
    }

    void ActivateProjectile()
    {
        // Активация снаряда из пула объектов.
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.IN_GAME)
        {
            projectile = projPool.GetObjectFromPool();
            Projectile projectileLogic = projectile.GetComponent<Projectile>();
            if (!projectileLogic.IsInitialize)
            {
                GetProjectileBehavior();
                projectileLogic.InitializeProjectile(movable, resetPosition, projectileDamage);
            }
        }
    }
    void GetProjectileBehavior()
    {
        // Инициализация стратегий снаряда.
        movable = new MoveForwardBehavior(projectile, speed: 0.2f, reachDistance: 0);
        resetPosition = new ResetPositionBehavior(projectile, movable, null);
    }
}
