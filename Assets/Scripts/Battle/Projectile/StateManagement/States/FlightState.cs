using StateManagement;

namespace Battle.Projectile.StateManagement.States
{
    public class FlightState : State<ProjectileContext>
    {
        public override void Update(float deltaTime)
        {
            Context.ScanCollision();
        }
    }
}