public interface IMovable
{
    public event GameObjectActionDelegate OnFinishingMove;
    public float Speed { get; }
    public void Move();
}
