using UnityEngine;

public interface IDetector
{
    public event TriggerDelegate OnTriggerAction;

    public event DetectionDelegate OnDetection;
    public GameObject Target { get; }
    public void DetectTarget();
}
