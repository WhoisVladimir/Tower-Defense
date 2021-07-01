public interface IShooterable 
{
    // »нтерфейс стрел€ющего объекта.
    public event TriggerDelegate OnTriggerAction;
    public event GameObjectsInteractionDelegate OnShot;

    public void GetProjectileData(IMovable movable);
    public void Shoot();
}
