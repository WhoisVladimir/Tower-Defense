using UnityEngine;

public class GuidedMoveBehavior : IMovable
{
    public event GameObjectActionDelegate OnFinishingMove;
    public bool IsInitialize { get; set; }
    public float Speed { get; private set; }

    const float m_reachDistance = 0.1f;
    GameObject obj;
    GameObject targetToMove;
    GameObject currentTarget;
    Vector3 targetPosition;
    Vector3 translation;
    bool isOnTheWay = false;

    public GuidedMoveBehavior(GameObject obj, IDetector detector, float speed = 0.5f)
    {
        this.obj = obj;
        Speed = speed;
        detector.OnDetection += DetectTarget;
    }
    public void Move()
    {
        if(currentTarget != null)
        {
            isOnTheWay = true;
            Vector3 curPosition = obj.transform.position;
            targetPosition = currentTarget.transform.position;

            if (currentTarget.activeInHierarchy == true && Vector3.Distance(curPosition, targetPosition) > m_reachDistance)
            {
                translation = targetPosition - curPosition;
                translation = translation.normalized * Speed;
                obj.transform.Translate(translation);
            }
            else 
            {
                OnFinishingMove?.Invoke(obj);
                isOnTheWay = false;
            }
        }
    }
    void DetectTarget(GameObject sender, GameObject target)
    {
        targetToMove = target;
        if (isOnTheWay == false) currentTarget = target;
    }
}
