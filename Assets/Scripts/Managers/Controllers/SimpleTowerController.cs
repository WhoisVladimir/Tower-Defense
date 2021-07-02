using UnityEngine;

public class SimpleTowerController : TowerController
{
    // ����� ����������� ��������� �����, ���������� � ������� ������.

    IDetector towDetection;
    IReturnable resetPosition;
    IMovable movable;
    IShooterable towerAttack;
    int projectileDamage = 10;

    public SimpleTowerController(GameObject projectile, GameObject tower)
        : base(projectile, tower)
    {
    }
    protected override void InitializeSystem()
    {
        // ������������� ��������� �����.

        towDetection = new NearestMotionDetectBehavior(tower);
        towerAttack = new SimpleShootBehaviour(tower, towDetection);

        SmartTower towLogic = tower.GetComponent<SmartTower>();
        towLogic.InitializeTower(towDetection, towerAttack, null);
        towerAttack.OnShot += Shooter_OnShot;

        projPool = new ObjectPool(projectile);
    }

    private void Shooter_OnShot(GameObject sender, GameObject target)
    {
        // ��������� ������� ��� �������.
        ActivateProjectile(target);
        projectile.SetActive(true);
    }

    void ActivateProjectile(GameObject target)
    {
        // ��������� ������� �� ����.
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.IN_GAME)
        {
            projectile = projPool.GetObjectFromPool();
            Projectile projectileLogic = projectile.GetComponent<Projectile>();
            if (!projectileLogic.IsInitialize)
            {
                GetProjectileBehavior(target);
                projectileLogic.InitializeProjectile(movable, resetPosition, projectileDamage);
            }
            else projectileLogic.Movable.PointToTarget(target);
        }
    }
    void GetProjectileBehavior(GameObject target)
    {
        // ������������� ��������� �������.
        movable = new GuidedMoveBehavior(projectile, target, speed: 0.2f);
        resetPosition = new ResetPositionBehavior(projectile, movable, null);
    }
}
