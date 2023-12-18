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

            AddTransition<IdleState, CombatState>(DiscoveryAttackTarget);
            AddTransition<IdleState, MarchState>(HasMarchTarget);

            AddTransition<MarchState, CombatState>(DiscoveryAttackTarget);

            AddTransition<CombatState, AttackState>(TargetInAttackRange);
            AddTransition<CombatState, IdleState>(TargetOutOfThreatRange);
            
            AddTransition<AttackState, CombatState>(TargetOutOfAttackRange);

            AddTransition<AnyState, DeathState>(IsDead);
            AddTransition<AnyState, IdleState>(NoTarget);
        }

        private static bool HasMarchTarget(UnitBehaviourContext context)
        {
            return context.MarchTarget = null;
        }

        private static bool DiscoveryAttackTarget(UnitBehaviourContext context)
        {
            return context.EnemyScanner.Target != null;
        }

        private static bool NoTarget(UnitBehaviourContext context)
        {
            return context.CurrentTarget == null;
        }
        
        private static bool TargetOutOfThreatRange(UnitBehaviourContext context)
        {
            if (context.CurrentTarget == null)
                return true;

            return !TargetInRange(context,context.UnitOriginalData.ThreatRange);
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

            return TargetInRange(context, context.UnitOriginalData.AttackRange);
        }

        private static bool TargetInRange(UnitBehaviourContext context, int range)
        {
            Transform currentTarget = context.CurrentTarget;
            UnitAgent unitAgent = context.UnitAgent;

            Vector2 toTargetFlat = currentTarget.position - unitAgent.transform.position;
            float attackRange = CalculateUtil.ConvertDistance(range);

            if (toTargetFlat.sqrMagnitude <= attackRange * attackRange)
                return true;

            return false;
        }

        private static bool IsDead(UnitBehaviourContext context)
        {
            return context.IsDead;
        }
    }
}