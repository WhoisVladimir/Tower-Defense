using UnityEngine;

public class GuidedMoveBehavior : IMovable
{
    // Реализация стратегии движения. Предполагает преследование цели.

    public event GameObjectActionDelegate OnFinishingMove;
    public event TriggerDelegate OnTriggerAction;

    public bool IsInitialize { get; set; }
    public float Speed { get; private set; }

    const float m_reachDistance = 0.1f;
    GameObject obj;
    GameObject targetToMove;
    Vector3 targetPosition;
    Vector3 curPosition;

    public GuidedMoveBehavior(GameObject obj, GameObject target, float speed = 0.5f)
    {
        this.obj = obj;
        Speed = speed;
        targetToMove = target;
    }
    public void Move()
    {
        // Самонаводящееся движение снаряда за целью.
        if(targetToMove != null)
        {
            curPosition = obj.transform.position;
            targetPosition = targetToMove.transform.position;
            float distance = Vector3.Distance(targetPosition, curPosition);

            if (targetToMove.activeInHierarchy == true && distance > m_reachDistance)
            {
                obj.transform.position = Vector3.MoveTowards(curPosition, targetPosition, Speed);
            }
            else 
            {
                OnFinishingMove?.Invoke(obj);
                targetToMove = null;
            }
        }
        else OnFinishingMove?.Invoke(obj);
    }
    public void PointToTarget(GameObject target)
    {
        // Ручной указатель на цель.
        targetToMove = target;
    }
}
