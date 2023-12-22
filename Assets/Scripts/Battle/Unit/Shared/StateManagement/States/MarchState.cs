namespace Battle.Unit.Shared.StateManagement.States
{
    public class MarchState : MoveState
    {
        public override void Enter()
        {
            base.Enter();
            Context.Target = Context.MarchTarget;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Context.ScanEnemy();
        }
    }
}