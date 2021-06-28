using UnityEngine;

public class MoveForwardBehavior : IMovable
{
    public float Speed { get; private set; }
    public event GameObjectActionDelegate OnFinishingMove;
    GameObject gameObject;
    Vector3 respawn;
    Vector3 destination;
    Vector3 translation;
    Vector3 direction;
    float reachDistance;
    float boundsDistance;

    public MoveForwardBehavior(GameObject gameObject, IDetector detector, float speed = 0.1f, float reachDistance = 0.1f, 
        float boundsDistance = 100f)
    {
        this.gameObject = gameObject;
        respawn = gameObject.transform.position;
        detector.OnDetection += SetDestinationPoint;
        Speed = speed;
        this.reachDistance = reachDistance;
        this.boundsDistance = boundsDistance;
    }
    public void Move()
    {
        Vector3 curPosition = gameObject.transform.position;
        float distance = Vector3.Distance(curPosition, destination);
        if ( distance > reachDistance)
        {
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

        float distance = Vector3.Distance(respawn, curPos);
        if (distance > boundsDistance)
        {
            return true;
        }
        return false;
    }
    void SetDestinationPoint(GameObject sender, GameObject target)
    {
        destination = target.transform.position;
        direction = (destination - respawn).normalized;
    }
}
