using UnityEngine;
public interface IReturnable
{
    // Интерфейс возвращающегося объекта.

    public event TriggerDelegate OnTriggerAction;
    public void ResetPosition(GameObject returnable);
}
