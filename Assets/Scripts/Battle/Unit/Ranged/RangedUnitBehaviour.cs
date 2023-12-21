using Battle.Projectile;
using Battle.Shared;
using Battle.Unit.Shared;
using UnityEngine;

namespace Battle.Unit.Ranged
{
    public class RangedUnitBehaviour : UnitBehaviour
    {
        public ProjectileShooter shooter;

        protected override void OnProvideSession(BattleSession session)
        {
            shooter.ProvideContainer(session.ProjectileContainer);
        }

        protected override void AttackDetection()
        {
            shooter.SetAttackDamage(Context.UnitEntity.AttackDamage);
            shooter.SetAttackLayerMask(Context.EnemyScanner.LayerMask);


            if (!Context.CurrentTarget.TryGetComponent(out IAttackTarget attackTarget))
            {
                return;
            }

            Vector2 from = agent.Center;
            Vector3 to = attackTarget.Center;
            shooter.Shoot(from, to);
        }
        
        
    }
}