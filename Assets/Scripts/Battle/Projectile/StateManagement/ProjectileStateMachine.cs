using Battle.Projectile.StateManagement.States;
using StateManagement;

namespace Battle.Projectile.StateManagement
{
    public class ProjectileStateMachine : StateMachine<ProjectileContext>
    {
        public ProjectileStateMachine(ProjectileContext context) : base(context)
        {
            AddState<FlightState>();
        }
    }
}