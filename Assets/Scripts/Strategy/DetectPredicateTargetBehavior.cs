using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPredicateTargetBehavior : IDetector
{
    public GameObject Target { get; private set; }
    GameObject gameObject;

    public event GameObjectsInteractionDelegate OnDetection;

    public DetectPredicateTargetBehavior(GameObject gameObject, GameObject target)
    {
        this.gameObject = gameObject;
        Target = target;
    }

    public void DetectTarget()
    {
        OnDetection?.Invoke(gameObject, Target);
    }
}
