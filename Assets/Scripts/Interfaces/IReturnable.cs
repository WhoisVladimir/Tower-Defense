using UnityEngine;
public interface IReturnable
{
    public event TriggerDelegate OnTriggerAction;
    public void ResetPosition(GameObject returnable);
}
