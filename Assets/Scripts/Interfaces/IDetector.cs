using UnityEngine;

public interface IDetector
{
    // Интерфейс обнаруживающего объекта.

    public event TriggerDelegate OnTriggerAction;

    public event DetectionDelegate OnDetection;
    public GameObject Target { get; }
    public void DetectTarget();
}
