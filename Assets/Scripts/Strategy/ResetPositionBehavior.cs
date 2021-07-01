using UnityEngine;

public class ResetPositionBehavior : IReturnable
{
    // ���������� ��������� ��������������� �������.
    public event TriggerDelegate OnTriggerAction;

    GameObject obj;
    Vector3 startPosition;
    public ResetPositionBehavior(GameObject gameObject, IMovable movable, IAttackable attackable)
    {
        obj = gameObject;
        startPosition = obj.transform.position;
        //Debug.Log($"In IReturnable: {gameObject.name} {gameObject.GetInstanceID()}");

        if (movable != null) movable.OnFinishingMove += ResetPosition;
        if (attackable != null) attackable.OnDeath += ResetPosition;
    }
    public void ResetPosition(GameObject sender)
    {
        // ����� ������� � ���������.

        sender.SetActive(false);
        sender.transform.position = startPosition;
    }
}
