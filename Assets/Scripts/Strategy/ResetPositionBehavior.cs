using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionBehavior : IReturnable
{
    public event TriggerDelegate OnTriggerAction;

    IMovable movable;
    IAttackable attackable;
    GameObject obj;
    Vector3 startPosition;
    public ResetPositionBehavior(GameObject gameObject, IMovable movable, IAttackable attackable)
    {
        obj = gameObject;
        startPosition = obj.transform.position;
        this.movable = movable;
        this.attackable = attackable;
        //Debug.Log($"In IReturnable: {gameObject.name} {gameObject.GetInstanceID()}");

        if (movable != null) movable.OnFinishingMove += ResetPosition;
        if (attackable != null) attackable.OnDeath += ResetPosition;
    }


    public void ResetPosition(GameObject sender)
    {
            sender.SetActive(false);
            sender.transform.position = startPosition;
    }
}
