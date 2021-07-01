using UnityEngine;

public class CannonTowerController : TowerController
{
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
        ActivateProjectile();
        shooterable.GetProjectileData(movable);
    }

    private void Shooterable_OnShot(GameObject sender, GameObject target)
    {
        ActivateProjectile();
        movable.PointToTarget(target);
        projectile.SetActive(true);
    }

    void ActivateProjectile()
    {
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
        movable = new MoveForwardBehavior(projectile, speed: 0.2f, reachDistance: 0);
        resetPosition = new ResetPositionBehavior(projectile, movable, null);
    }
}
