using UnityEngine;

public class PeacefulEnemyController : EnemiesController
{
    int monsterHP = 30;

    IAttackable attackable;
    IMovable movable;
    IReturnable returnable;

    public PeacefulEnemyController(GameObject enemy, GameObject endPoin) : base(enemy, endPoin)
    {
    }

    public override void SpawnLoop()
    {
        // Спавн монстров.

        if (GameManager.Instance.CurrentGameState == GameManager.GameState.IN_GAME)
        {
            GameObject monsterInst = enemiesPool.GetObjectFromPool();
            Monster monster = monsterInst.GetComponent<Monster>();
            if (!monster.IsInitialize)
            {
                attackable = new TakingDamageBehavior(monsterInst, monsterHP);
                movable = new MoveForwardBehavior(monsterInst);
                returnable = new ResetPositionBehavior(monsterInst, movable, attackable);
                monster.InitializeMonster(attackable, movable, returnable, null);
                movable.PointToTarget(endPoint);
            }
            monsterInst.SetActive(true);
        }
    }
}
