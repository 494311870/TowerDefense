using Battle.Projectile.StateManagement.States;
using StateManagement;

namespace Battle.Projectile.StateManagement
{
    public class ProjectileStateMachine : StateMachine<ProjectileContext>
    {
        public ProjectileStateMachine(ProjectileContext context) : base(context)
        {
            AddState<PrepareState>();
            AddState<FlightState>();
            AddState<DeathState>();

            SetRootState<PrepareState>();

            AddTransition<PrepareState, FlightState>(IsReady);
            AddTransition<FlightState, DeathState>(IsConsumed);
        }

        private static bool IsReady(ProjectileContext context)
        {
            return context.ProjectileEntity.IsReady;
        }

        private static bool IsConsumed(ProjectileContext context)
        {
            return context.IsConsumed;
        }
    }
}