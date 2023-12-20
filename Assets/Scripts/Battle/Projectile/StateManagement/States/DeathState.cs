using StateManagement;

namespace Battle.Projectile.StateManagement.States
{
    public class DeathState : State<ProjectileContext>
    {
        public override void Enter()
        {
            Context.ProjectileAgent.WaitingInPlace();
            Context.ProjectileAgent.Death();
        }
    }
}