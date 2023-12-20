using Battle.Projectile;
using Battle.Unit.Shared;
using UnityEngine;

namespace Battle.Unit.Ranged
{
    public class RangedUnitBehaviour : UnitBehaviour
    {
        public ProjectileShooter shooter;

        protected override void AttackDetection()
        {
            Vector2 from = agent.Center;
            Vector3 to = Context.CurrentTarget.position;
            shooter.Shoot(from, to);
        }
    }
}