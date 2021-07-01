using UnityEngine;
public interface IMovable
{
    public event TriggerDelegate OnTriggerAction;
    public event GameObjectActionDelegate OnFinishingMove;
    public float Speed { get; }
    public void Move();
    public void PointToTarget(GameObject target);
}
