namespace StateManagement
{
    public abstract class State<T> : IState where T : class
    {
        public T Context { get; set; }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update(float deltaTime)
        {
        }
    }
}