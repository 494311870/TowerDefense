using StateManagement;

namespace Battle.Projectile.StateManagement.States
{
    public class PrepareState : State<ProjectileContext>
    {
        public override void Enter()
        {
            Context.ProjectileEntity.RePrepare();
            Context.ProjectileAgent.WaitingInPlace();
            Context.ProjectileAgent.transform.position = Context.StartPosition;
        }

        public override void Update(float deltaTime)
        {
            Context.ProjectileEntity.Prepare(deltaTime);
        }
    }
}