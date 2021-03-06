using UnityEngine;

public class PreemptiveShootBehavior : IShooterable
{
    //?????????? ????????? ????????. ????????? ?????????? ?????????? ?? ?????? ?????????? ??????.

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
        // ????????? ?????????? ? ???? ?????.

        this.target = target;
        targetDirection = direction;
        targetSpeed = speed;
        OnTriggerAction?.Invoke();
    }

    public void GetProjectileData(IMovable movable)
    {
        //????????? ?????????? ? ???????? ???????.

        projectileSpeed = movable.Speed;
    }

    void CalculatePreemtivePosition()
    {
        //?????? ??????????? ??????????. 

        Vector3 targetPosition = target.transform.position;
        Vector3 shotPosition = gameObject.transform.position; 
        float distanceToZero = Vector3.Distance(targetPosition, shotPosition);
        Vector3 direct = targetPosition - shotPosition;
        Vector3 targetVelocity = targetDirection * targetSpeed;
       
        float targetMoveAngle = Vector3.Angle(-direct, targetVelocity) * Mathf.Deg2Rad;
        float shootAngle = Mathf.Asin(Mathf.Sin(targetMoveAngle) * targetVelocity.magnitude / projectileSpeed);
        
        aim.transform.position = targetPosition + targetVelocity * distanceToZero / Mathf.Sin(
            Mathf.PI - targetMoveAngle - shootAngle) * Mathf.Sin(shootAngle) / targetVelocity.magnitude;
    }
    public void Shoot()
    {
        // ???????? ??????? ? ????????.
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
