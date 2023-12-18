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
            AddState<AttackState>();
            AddState<DeathState>();
            AddState<PursuitState>();

            AddTransition<PursuitState, AttackState>(TargetInAttackRange);
            AddTransition<AttackState, PursuitState>(TargetOutOfAttackRange);
            AddTransition<IdleState, PursuitState>(DiscoveryTarget);


            AddTransition<AnyState, DeathState>(IsDead);
            AddTransition<AnyState, IdleState>(NoTarget);
            AddTransition<AnyState, PursuitState>(HasTarget);
        }

   

        private bool DiscoveryTarget(UnitBehaviourContext context)
        {
            context.ScanEnemy();
            return context.EnemyScanner.Target != null;            
        }

        private bool NoTarget(UnitBehaviourContext context)
        {
            return context.CurrentTarget == null;
        }
        
        private bool HasTarget(UnitBehaviourContext context)
        {
            return context.CurrentTarget != null;
        }

        private static bool TargetOutOfAttackRange(UnitBehaviourContext context)
        {
            return !TargetInAttackRange(context);
        }

        private static bool TargetInAttackRange(UnitBehaviourContext context)
        {
            Transform currentTarget = context.CurrentTarget;
            // UnitEntity unitEntity = context.UnitEntity;
            IReadonlyUnitData unitData = context.UnitOriginalData;
            UnitAgent unitAgent = context.UnitAgent;

            if (currentTarget != null)
            {
                Vector2 toTargetFlat = currentTarget.position - unitAgent.transform.position;
                float attackRange = CalculateUtil.ConvertDistance(unitData.AttackRange);

                if (toTargetFlat.sqrMagnitude <= attackRange * attackRange)
                    return true;
            }

            return false;
        }

        private static bool IsDead(UnitBehaviourContext context)
        {
            return context.IsDead;
        }
    }
}