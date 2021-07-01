using UnityEngine;

public class PreemptiveShootBehavior : IShooterable
{
    //Реализация стратегии стрельбы. Вычисляет траекторию упреждения на основе полученных данных.

    public event GameObjectsInteractionDelegate OnShot;
    public event TriggerDelegate OnTriggerAction;

    GameObject gameObject;
    GameObject target;
    GameObject aim;
    float timer = 0.5f;
    float shootInterval;
    float range;
    float targetSpeed;
    float projectileSpeed;
    Vector3 targetDirection;

    public PreemptiveShootBehavior(GameObject gameObject, IDetector detector, float range = 20f, float shootInterval = 0.5f)
    {
        this.gameObject = gameObject;
        this.range = range;
        this.shootInterval = shootInterval;
        detector.OnDetection += GetTarget;
        aim = new GameObject("Aim");
    }
    void GetTarget(GameObject sender, GameObject target, Vector3 direction, float speed)
    {
        // Получение информации о цели извне.

        this.target = target;
        targetDirection = direction;
        targetSpeed = speed;
        OnTriggerAction?.Invoke();
    }

    public void GetProjectileData(IMovable movable)
    {
        //Получение информации о скорости снаряда.

        projectileSpeed = movable.Speed;
    }

    void CalculatePreemtivePosition()
    {
        //Расчёт упреждающей траектории. 

        Vector3 targetPosition = target.transform.position;
        Vector3 shotPosition = gameObject.transform.position; 
        float distanceToZero = Vector3.Distance(targetPosition, shotPosition);
        float time = distanceToZero / projectileSpeed;
        float targetDistance = time * targetSpeed;
        aim.transform.position = ((targetDistance / distanceToZero) * targetDistance * targetDirection) + targetPosition;  
    }
    public void Shoot()
    {
        // Проверка допуска к стрельбе.
        if (target != null && target.activeInHierarchy)
        {
            float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);

            if (distance < range + 1 && timer >= shootInterval)
            {
                CalculatePreemtivePosition();
                OnShot?.Invoke(gameObject, aim);
                timer = 0f;
            }
        }
        timer += Time.deltaTime;
    }
}
