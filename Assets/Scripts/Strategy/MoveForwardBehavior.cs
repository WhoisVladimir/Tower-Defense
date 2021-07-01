using UnityEngine;

public class MoveForwardBehavior : IMovable
{
    //Реализация стратегии движения. Предполагает простое движение вперёд, к указанной цели.
    public float Speed { get; private set; }
    public event GameObjectActionDelegate OnFinishingMove;
    public event TriggerDelegate OnTriggerAction;

    GameObject gameObject;
    Vector3 respawn;
    Vector3 destination;
    Vector3 translation;
    Vector3 direction;
    float reachDistance;
    float boundsDistance;

    public MoveForwardBehavior(GameObject gameObject, float speed = 0.1f, float reachDistance = 0.1f, 
        float boundsDistance = 75f)
    {
        this.gameObject = gameObject;
        respawn = gameObject.transform.position;
        Speed = speed;
        this.reachDistance = reachDistance;
        this.boundsDistance = boundsDistance;
    }
    public void Move()
    {
        // Движение по прямой в указанную точку.

        Vector3 curPosition = gameObject.transform.position;
        float distance = Vector3.Distance(curPosition, destination);
        if ( distance > reachDistance)
        {
            direction = (destination - respawn).normalized;
            translation = direction * Speed;
            gameObject.transform.Translate(translation);
            if (WentBeyondBoundaries(curPosition))
            {
                OnFinishingMove?.Invoke(gameObject);
            }
        }
        else
        {
            OnFinishingMove?.Invoke(gameObject);
        }
    }
    public bool WentBeyondBoundaries(Vector3 curPos)
    {
        // Проверка на выход за границы.
        float distance = Vector3.Distance(respawn, curPos);
        if (distance > boundsDistance)
        {
            return true;
        }
        return false;
    }

    public void PointToTarget(GameObject target)
    {
        // Ручной указатель на цель.
        destination = target.transform.position;
    }
}
