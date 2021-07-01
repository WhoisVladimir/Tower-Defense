using UnityEngine;

public class NearestMotionDetectBehavior : IDetector
{
    // Реализация стратегии отслеживания. Отслеживает ближайшую цель, анализирует её, и сообщает о событии.

    public event DetectionDelegate OnDetection;
    public event TriggerDelegate OnTriggerAction;

    public GameObject Target { get; private set; }

    GameObject obj;
    Vector3 center;
    Vector3 startPosition;
    Vector3 direction;
    float range;
    float targetSpeed;
    bool wasAnalyzed = false;

    public NearestMotionDetectBehavior(GameObject obj, float range = 20f)
    {
        this.obj = obj;
        this.range = range;
        center = obj.transform.position;
    }
    public void DetectTarget()
    {
        // Обнаружение ближайшей цели.
        if (Target == null)
        {
            int maxColliders = 5;
            int enemyLayer = 1 << 3;
            Collider[] colliders = new Collider[maxColliders];
            int numColliders = Physics.OverlapSphereNonAlloc(center, range, colliders, enemyLayer);
            if (numColliders > 0)
            {
                float nearestDistance = range + 1;
                int num = 0;
                for (int i = 0; i < numColliders; i++)
                {
                    float distance = Vector3.Distance(center, colliders[i].transform.position);
                    if (nearestDistance > distance)
                    {
                        nearestDistance = distance;
                        num = i;
                    }
                }
                Target = colliders[num].gameObject;
                startPosition = Target.transform.position;
            }
        }
        else 
        {
            if (wasAnalyzed == false) GetTargetData();
            ZeroingTarget();
        }
    }
    private void ZeroingTarget()
    {
        // Сброс цели.
        float distance = Vector3.Distance(obj.transform.position, Target.transform.position);
        if (distance > range + 1 || Target.activeInHierarchy == false)
        {
            Target = null;
            wasAnalyzed = false;
        }
    }
    private void GetTargetData()
    {
        // Анализ данных цели.
        Vector3 curPosition = Target.transform.position;
        direction = (curPosition - startPosition).normalized;
        targetSpeed = Vector3.Distance(curPosition, startPosition);
        wasAnalyzed = true;
        OnDetection?.Invoke(obj, Target, direction, targetSpeed);
    }
}
