using System.Collections;
using UnityEngine;

public enum TowerType 
{
    ShootingMagic,
    ShootingFirearm
}

public enum MonsterType
{
    PeacefulPasserby
}

public class ObjectManager : Singleton<ObjectManager>
{
    [SerializeField] GameObject capsuleMonster;
    [SerializeField] GameObject cannonProjectile;
    [SerializeField] GameObject magicProjectile;
    [SerializeField] GameObject magicTower;
    [SerializeField] GameObject cannonTower;
    [SerializeField] GameObject cannonStartPoint;
    [SerializeField] GameObject guidedStartPoint;
    [SerializeField] GameObject endPoint;

    private void Start()
    {
        TowerController cannon = GetTower(TowerType.ShootingFirearm);
        TowerController magicCrystal = GetTower(TowerType.ShootingMagic);
        
        EnemiesController evilCapsule = GetEnemy(MonsterType.PeacefulPasserby);

        cannon.Start();
        magicCrystal.Start();
        
        evilCapsule.Start();
        StartCoroutine(SpawnEnemies(evilCapsule));
    }

    TowerController GetTower(TowerType type)
    {
        // Возвращает доступный тип башни.
        switch (type)
        {
            case TowerType.ShootingMagic:
                return new MagicTowerController(magicProjectile, magicTower, guidedStartPoint);
            case TowerType.ShootingFirearm:
                return new CannonTowerController(cannonProjectile, cannonTower, cannonStartPoint);
            default:
                Debug.Log("Unknown type");
                return null;
        }
    }

    EnemiesController GetEnemy(MonsterType type)
    {
        // Возвращает доступный тип монстра.
        switch (type)
        {
            case MonsterType.PeacefulPasserby:
                return new PeacefulEnemyController(capsuleMonster, endPoint);
            default:
                Debug.Log("Unknown type");
                return null;
        }
    }

    IEnumerator SpawnEnemies(EnemiesController controller)
    {
        //Спавн монстров.
        while (GameManager.Instance.CurrentGameState == GameManager.GameState.IN_GAME)
        {
            controller.SpawnLoop();
            yield return new WaitForSeconds(3);
        }
    }
}
