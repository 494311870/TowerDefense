using Battle.Shared;
using StateManagement;
using UnityEngine;

namespace Battle.Projectile.StateManagement.States
{
    public class FlightState : State<ProjectileContext>
    {
        public override void Enter()
        {
            Vector2 direction = Context.EndPosition - Context.StartPosition;
            Vector2 velocity = direction.normalized * Context.ProjectileEntity.MoveSpeed;
            Context.ProjectileAgent.InputVelocity(velocity);
        }

        public override void Update(float deltaTime)
        {
            Context.ScanCollision();
            CircleTargetScanner hitScanner = Context.HitScanner;
            if (hitScanner.Target != null)
            {
                Context.IsConsumed = true;
                if (hitScanner.Target.TryGetComponent(out IAttackTarget attackTarget))
                {
                    attackTarget.Hurt(Context.ProjectileEntity.AttackDamage);
                }
            }
        }
    }
}