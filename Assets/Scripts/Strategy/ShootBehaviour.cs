using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : IShooterable
{
    public event GameObjectsInteractionDelegate OnShot;
    GameObject gameObject;
    GameObject target;
    float range;
    float shootInterval;
    float timer;
    public ShootBehaviour(GameObject gameObject, IDetector detector, float range = 20f, float shootInterval = 0.5f)
    {
        this.gameObject = gameObject;
        this.range = range;
        this.shootInterval = shootInterval;
        timer = shootInterval;
        detector.OnDetection += GetTarget;
    }
    private void GetTarget(GameObject sender, GameObject target)
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
                Debug.Log($"On shoot {gameObject.name} to {target.name} {target.GetInstanceID()}");
                OnShot?.Invoke(gameObject, target);
                timer = 0f;
            }
        }
        timer += Time.deltaTime;
    }
}
