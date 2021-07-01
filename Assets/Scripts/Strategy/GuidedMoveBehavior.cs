using UnityEngine;

public class GuidedMoveBehavior : IMovable
{
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
                targetToMove = null;
                OnFinishingMove?.Invoke(obj);
            }
        }
    }
    public void PointToTarget(GameObject target)
    {
        targetToMove = target;
    }
}
