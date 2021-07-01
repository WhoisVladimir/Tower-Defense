using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByTrajectory : IMovable
{
    public event GameObjectActionDelegate OnFinishingMove;
    public event TriggerDelegate OnTriggerAction;

    public float Speed { get; private set; }
    GameObject gameObject;
    GameObject target;
    Rigidbody rb;
    Vector3 startPosition;
    Vector3 targetPosition;

    public MoveByTrajectory(GameObject gameObject, GameObject target, float speed = 0.2f)
    {
        this.gameObject = gameObject;
        this.target = target;
        Speed = speed;
        startPosition = gameObject.transform.position;
        targetPosition = target.transform.position;
        rb = gameObject.AddComponent<Rigidbody>();
    }

    public void Move()
    {
        throw new System.NotImplementedException();
    }

    public void PointToTarget(GameObject target)
    {
        throw new System.NotImplementedException();
    }
    public void GetParabolicMove( )
    {
        Vector3 distance = targetPosition = startPosition;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;
    }
}
