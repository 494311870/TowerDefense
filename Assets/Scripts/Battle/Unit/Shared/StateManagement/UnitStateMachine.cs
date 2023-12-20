using Battle.Shared;
using Battle.Unit.Shared.StateManagement.States;
using StateManagement;
using UnityEngine;

namespace Battle.Unit.Shared.StateManagement
{
    public class UnitStateMachine : StateMachine<UnitContext>
    {
        public UnitStateMachine(UnitContext context) : base(context)
        {
            AddState<IdleState>();
            AddState<MarchState>();
            AddState<CombatState>();
            AddState<AttackState>();
            AddState<DeathState>();
            
            SetRootState<IdleState>();

            AddTransition<IdleState, CombatState>(HasAttackTarget);
            AddTransition<IdleState, MarchState>(HasMarchTarget);

            AddTransition<MarchState, CombatState>(HasAttackTarget);

            AddTransition<CombatState, AttackState>(TargetInAttackRange);
            AddTransition<CombatState, IdleState>(NoAttackTarget);

            AddTransition<AttackState, CombatState>(TargetOutOfAttackRange);

            AddTransition<AnyState, DeathState>(IsDead);
        }

        private static bool HasMarchTarget(UnitContext context)
        {
            return context.MarchTarget != null;
        }

        private static bool HasAttackTarget(UnitContext context)
        {
            return context.EnemyScanner.Target != null;
        }

        private static bool NoAttackTarget(UnitContext context)
        {
            return context.EnemyScanner.Target == null;
        }

        private static bool TargetOutOfAttackRange(UnitContext context)
        {
            return !TargetInAttackRange(context);
        }

        private static bool TargetInAttackRange(UnitContext context)
        {
            Transform currentTarget = context.CurrentTarget;
            if (currentTarget == null)
                return false;

            return TargetInRange(context, context.UnitEntity.AttackRange);
        }

        private static bool TargetInRange(UnitContext context, int checkRange)
        {
            Transform currentTarget = context.CurrentTarget;
            UnitAgent unitAgent = context.UnitAgent;

            Vector2 toTargetFlat = currentTarget.position - unitAgent.transform.position;
            float distance = CalculateUtil.ConvertToWorldDistance(checkRange);

            return toTargetFlat.sqrMagnitude <= distance * distance;
        }

        private static bool IsDead(UnitContext context)
        {
            return context.UnitEntity.IsDead;
        }
    }
}