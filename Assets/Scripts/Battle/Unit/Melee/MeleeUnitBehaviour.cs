using Battle.Shared;
using Battle.Unit.Shared;
using UnityEngine;

namespace Battle.Unit.Melee
{
    public class MeleeUnitBehaviour : UnitBehaviour
    {
        private CircleTargetScanner _attackScanner;

        protected override void OnInit()
        {
            _attackScanner = new CircleTargetScanner();
        }

        protected override void AttackDetection()
        {
            Debug.Log("AttackDetection");

            _attackScanner.LayerMask = Context.EnemyScanner.LayerMask;
            _attackScanner.ScanRange = Context.UnitEntity.AttackRange;
            _attackScanner.Scan(agent.Center);

            Collider2D target = _attackScanner.Target;
            if (target == null)
                return;

            if (target.TryGetComponent(out IAttackTarget attackTarget))
                attackTarget.Hurt(Context.UnitEntity.AttackDamage);
        }
    }
}