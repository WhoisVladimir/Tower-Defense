using UnityEngine;

public class NearestMotionDetectBehavior : IDetector
{
    public event GameObjectsInteractionDelegate OnDetection;
    GameObject obj;
    public GameObject Target { get; private set; }
    Vector3 center;
    float range;
    public NearestMotionDetectBehavior(GameObject obj, float range = 20f)
    {
        this.obj = obj;
        this.range = range;
        center = obj.transform.position;
    }
    public void DetectTarget()
    {
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
                Debug.Log($"{obj.name} aim {Target.name} {Target.GetInstanceID()}");
                OnDetection?.Invoke(obj, Target);
            }
        }
        else if (Target !=null)
        {
            float distance = Vector3.Distance(obj.transform.position, Target.transform.position);
            if(distance > range + 1 || Target.activeInHierarchy == false)
            {
                Target = null;
            }
        }
    }
        

}
