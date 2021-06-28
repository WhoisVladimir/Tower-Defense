using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class EnemiesController 
{
    GameObject enemy;
    GameObject endPoint;
    ObjectPool enemiesPool;
    int monsterHP = 30;

    public EnemiesController(GameObject enemy, GameObject endPoin)
    {
        this.enemy = enemy;
        this.endPoint = endPoin;
    }
    public void Start()
    {
        ActivatePools();
        endPoint = Object.Instantiate(endPoint);
    }
    void ActivatePools()
    {
        enemiesPool = new ObjectPool(enemy);
    }
    public void SpawnLoop()
    {
        if (/*GameManager.Instance.CurrentGameState == GameManager.GameState.IN_GAME*/true)
        {
            GameObject monsterInst = enemiesPool.GetObjectFromPool();
            Monster monster = monsterInst.GetComponent<Monster>();
            if (monster.IsInitialize)
            {
                monsterInst.SetActive(true);
            }
            else
            {
                TakingDamageBehavior simpleTakingDamage = new TakingDamageBehavior(monsterInst, monsterHP);
                DetectPredicateTargetBehavior detectPredicateTarget = new DetectPredicateTargetBehavior(enemy, endPoint);
                MoveForwardBehavior move = new MoveForwardBehavior(monsterInst, detectPredicateTarget);
                ResetPositionBehavior resetPos = new ResetPositionBehavior(monsterInst, move, simpleTakingDamage);
                monster.InitializeMonster(simpleTakingDamage, move, resetPos, detectPredicateTarget);
                monsterInst.SetActive(true);
            }
            Debug.Log($"{monsterInst.name} {monsterInst.GetInstanceID()} was spawned");
             
        }
    }
}
