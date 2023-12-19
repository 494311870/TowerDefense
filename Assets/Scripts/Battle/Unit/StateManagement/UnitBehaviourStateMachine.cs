using Battle.Shared;
using Battle.Unit.StateManagement.States;
using StateManagement;
using UnityEngine;

namespace Battle.Unit.StateManagement
{
    public class UnitBehaviourStateMachine : StateMachine<UnitBehaviourContext>
    {
        public UnitBehaviourStateMachine(UnitBehaviourContext context) : base(context)
        {
            AddState<IdleState>();
            AddState<MarchState>();
            AddState<CombatState>();
            AddState<AttackState>();
            AddState<DeathState>();

            AddTransition<IdleState, CombatState>(HasAttackTarget);
            AddTransition<IdleState, MarchState>(HasMarchTarget);

            AddTransition<MarchState, CombatState>(HasAttackTarget);

            AddTransition<CombatState, AttackState>(TargetInAttackRange);
            AddTransition<CombatState, IdleState>(NoAttackTarget);

            AddTransition<AttackState, CombatState>(TargetOutOfAttackRange);

            AddTransition<AnyState, DeathState>(IsDead);

            Enter<IdleState>();
        }

        private static bool HasMarchTarget(UnitBehaviourContext context)
        {
            return context.MarchTarget != null;
        }

        private static bool HasAttackTarget(UnitBehaviourContext context)
        {
            return context.EnemyScanner.Target != null;
        }

        private static bool NoAttackTarget(UnitBehaviourContext context)
        {
            return context.EnemyScanner.Target == null;
        }

        private static bool TargetOutOfAttackRange(UnitBehaviourContext context)
        {
            return !TargetInAttackRange(context);
        }

        private static bool TargetInAttackRange(UnitBehaviourContext context)
        {
            Transform currentTarget = context.CurrentTarget;
            if (currentTarget == null)
                return false;

            return TargetInRange(context, context.UnitEntity.AttackRange);
        }

        private static bool TargetInRange(UnitBehaviourContext context, int checkRange)
        {
            Transform currentTarget = context.CurrentTarget;
            UnitAgent unitAgent = context.UnitAgent;

            Vector2 toTargetFlat = currentTarget.position - unitAgent.transform.position;
            float distance = CalculateUtil.ConvertToWorldDistance(checkRange);

            return toTargetFlat.sqrMagnitude <= distance * distance;
        }

        private static bool IsDead(UnitBehaviourContext context)
        {
            return context.UnitEntity.IsDead;
        }
    }
}