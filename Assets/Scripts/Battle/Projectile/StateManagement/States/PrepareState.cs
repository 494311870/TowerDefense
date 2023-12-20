using StateManagement;

namespace Battle.Projectile.StateManagement.States
{
    public class PrepareState : State<ProjectileContext>
    {
        public override void Enter()
        {
            Context.ProjectileEntity.RePrepare();
            Context.ProjectileAgent.WaitingInPlace();
        }

        public override void Update(float deltaTime)
        {
            Context.ProjectileEntity.Prepare(deltaTime);
        }
    }
}