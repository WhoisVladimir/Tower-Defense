using UnityEngine;
public interface IMovable
{
    // Интерфейс движущегося объекта.

    public event TriggerDelegate OnTriggerAction;
    public event GameObjectActionDelegate OnFinishingMove;
    public float Speed { get; }
    public void Move();
    public void PointToTarget(GameObject target);
}
