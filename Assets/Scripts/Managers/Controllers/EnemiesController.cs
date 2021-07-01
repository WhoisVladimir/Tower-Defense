using UnityEngine;

public abstract class EnemiesController 
{
    // Абстрактный общий класс врагов.
    protected GameObject enemy;
    protected GameObject endPoint;
    protected ObjectPool enemiesPool;

    public EnemiesController(GameObject enemy, GameObject endPoint)
    {
        this.enemy = enemy;
        this.endPoint = endPoint;
    }
    public void Start()
    {
        enemiesPool = new ObjectPool(enemy);
        endPoint = Object.Instantiate(endPoint);
    }
    public abstract void SpawnLoop();
}
