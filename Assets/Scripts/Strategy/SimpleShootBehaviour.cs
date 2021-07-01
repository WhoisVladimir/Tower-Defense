using UnityEngine;

public class SimpleShootBehaviour : IShooterable
{
    //Стратегия стрельбы. Реализует простую стрельбу.

    public event GameObjectsInteractionDelegate OnShot;
    public event TriggerDelegate OnTriggerAction;

    GameObject gameObject;
    GameObject target;
    float range;
    float shootInterval;
    float timer;

    public SimpleShootBehaviour(GameObject gameObject, IDetector detector, float range = 20f, float shootInterval = 0.5f)
    {
        this.gameObject = gameObject;
        this.range = range;
        this.shootInterval = shootInterval;
        timer = shootInterval;
        detector.OnDetection += GetTarget;
    }
    private void GetTarget(GameObject sender, GameObject target, Vector3 direction, float speed)
    {
        this.target = target;
    }

    public void Shoot()
    {

        if (target != null && target.activeInHierarchy)
        {
            float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);

            if (distance < range + 1 && timer >= shootInterval) 
            {
                OnShot?.Invoke(gameObject, target);
                timer = 0f;
            }
        }
        timer += Time.deltaTime;
    }

    public void GetProjectileData(IMovable movable)
    {
        throw new System.NotImplementedException();
    }
}
