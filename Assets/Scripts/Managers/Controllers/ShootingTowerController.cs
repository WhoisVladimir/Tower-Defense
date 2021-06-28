using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTowerController : TowerController
{
    TowerType towerType;
    IDetector towDetection;
    IReturnable resetPosition;
    IMovable movable;
    int projectileDamage = 10;

    public ShootingTowerController(GameObject projectile, GameObject tower, GameObject projectileRespawn, TowerType type )
        : base(projectile, tower, projectileRespawn)
    {
        towerType = type;
    }
  
    protected override void InitializeSystem()
    {
        towDetection = new NearestMotionDetectBehavior(tower);
        ShootBehaviour towerAttack = new ShootBehaviour(tower, towDetection);

        SmartTower towLogic = tower.GetComponent<SmartTower>();
        towLogic.InitializeTower(towDetection, towerAttack, null);
        towDetection.OnDetection += Detector_OnDetection;
        towerAttack.OnShot += Shooter_OnShot;

        projPool = new ObjectPool(projectile);
    }

    private void Detector_OnDetection(GameObject sender, GameObject target)
    {
        //Debug.Log($"{sender.name} {sender.GetInstanceID()} detect {target.name} {target.GetInstanceID()}");
        ActivateProjectile();
    }
    void ActivateProjectile()
    {
        if (true/*GameManager.Instance.CurrentGameState == GameManager.GameState.IN_GAME*/)
        {
            projectile = projPool.GetObjectFromPool();
            //Debug.Log($"Instance from pool: {projInstance.GetInstanceID()}");
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
        movable = GetMoveBehavior();
        resetPosition = new ResetPositionBehavior(projectile, movable, null);
    }
    IMovable GetMoveBehavior()
    {
        switch (towerType)
        {
            case TowerType.ShootingMagic:
                return new GuidedMoveBehavior(projectile, towDetection, speed: 0.2f);
            case TowerType.ShootingFirearm:
                return new MoveForwardBehavior(projectile, towDetection, speed: 0.2f, reachDistance: 0);
            default:
                return null;
        }
    }
    private void Shooter_OnShot(GameObject sender, GameObject target)
    {
        projectile.SetActive(true);
        //Debug.Log($"{sender.name} {sender.GetInstanceID()} shoot into {target.name} {target.GetInstanceID()}");
        //Debug.Log($"{projectile.name} {projectile.GetInstanceID()} flies to capsule {target.GetInstanceID()}");
    }
}
