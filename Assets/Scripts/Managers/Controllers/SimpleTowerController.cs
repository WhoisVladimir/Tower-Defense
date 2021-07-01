using UnityEngine;

public class SimpleTowerController : TowerController
{
    // Класс реализующий поведение башни, стреляющей в обычном режиме.

    IDetector towDetection;
    IReturnable resetPosition;
    IMovable movable;
    IShooterable towerAttack;
    int projectileDamage = 10;

    public SimpleTowerController(GameObject projectile, GameObject tower, GameObject projectileRespawn)
        : base(projectile, tower, projectileRespawn)
    {
    }
    protected override void InitializeSystem()
    {
        // Инициализация стратегий башни.

        towDetection = new NearestMotionDetectBehavior(tower);
        towerAttack = new SimpleShootBehaviour(tower, towDetection);

        SmartTower towLogic = tower.GetComponent<SmartTower>();
        towLogic.InitializeTower(towDetection, towerAttack, null);
        towerAttack.OnShot += Shooter_OnShot;

        projPool = new ObjectPool(projectile);
    }

    private void Shooter_OnShot(GameObject sender, GameObject target)
    {
        // активация снаряда для запуска.
        ActivateProjectile(sender, target);
        projectile.SetActive(true);
    }

    void ActivateProjectile(GameObject sender, GameObject target)
    {
        // Получение снаряда из пула.
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.IN_GAME)
        {
            projectile = projPool.GetObjectFromPool();
            Projectile projectileLogic = projectile.GetComponent<Projectile>();
            if (!projectileLogic.IsInitialize)
            {
                GetProjectileBehavior(sender, target);
                projectileLogic.InitializeProjectile(movable, resetPosition, projectileDamage);
            }
            movable.PointToTarget(target);
        }
    }
    void GetProjectileBehavior(GameObject sender, GameObject target)
    {
        // Инициализация стратегий снаряда.
        movable = new GuidedMoveBehavior(projectile, target, speed: 0.2f);
        resetPosition = new ResetPositionBehavior(projectile, movable, null);
    }
}
