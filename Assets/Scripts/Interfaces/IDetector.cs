using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetector
{
    public event GameObjectsInteractionDelegate OnDetection;
    public GameObject Target { get; }
    public void DetectTarget();
}
