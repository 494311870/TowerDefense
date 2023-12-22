using Battle.Shared;
using Battle.Unit.Shared.StateManagement.States;
using StateManagement;
using UnityEngine;

namespace Battle.Unit.Shared.StateManagement
{
    public class UnitStateMachineBuilder : IStateMachineBuilder<UnitContext>
    {
        public void BuildStates(StateMachine<UnitContext> stateMachine)
        {
            stateMachine.AddState<IdleState>();
            stateMachine.AddState<MarchState>();
            stateMachine.AddState<CombatState>();
            stateMachine.AddState<AttackState>();
            stateMachine.AddState<DeathState>();
            stateMachine.SetRootState<IdleState>();
        }

        public void BuildTransitions(StateMachine<UnitContext> stateMachine)
        {
            stateMachine.AddTransition<IdleState, CombatState>(HasAttackTarget);
            stateMachine.AddTransition<IdleState, MarchState>(HasMarchTarget);

            stateMachine.AddTransition<MarchState, CombatState>(HasAttackTarget);

            stateMachine.AddTransition<CombatState, AttackState>(TargetInAttackRange);
            stateMachine.AddTransition<CombatState, IdleState>(NoAttackTarget);

            stateMachine.AddTransition<AttackState, CombatState>(TargetOutOfAttackRange);

            stateMachine.AddTransition<AnyState, DeathState>(IsDead);
        }

        private static bool HasMarchTarget(UnitContext context)
        {
            return context.MarchTarget != null;
        }

        private static bool HasAttackTarget(UnitContext context)
        {
            return context.EnemyScanner.IsDetected;
        }

        private static bool NoAttackTarget(UnitContext context)
        {
            return !context.EnemyScanner.IsDetected;
        }

        private static bool TargetOutOfAttackRange(UnitContext context)
        {
            return !TargetInAttackRange(context);
        }

        private static bool TargetInAttackRange(UnitContext context)
        {
            return TargetInRange(context, context.UnitEntity.AttackRange);
        }

        private static bool TargetInRange(UnitContext context, int checkRange)
        {
            ITarget currentTarget = context.Target;
            if (currentTarget == null || currentTarget.IsInvalid)
                return false;

            UnitAgent unitAgent = context.UnitAgent;

            Vector2 toTargetFlat = currentTarget.Position - unitAgent.Position;
            float distance = CalculateUtil.ConvertToWorldDistance(checkRange);

            return toTargetFlat.sqrMagnitude <= distance * distance;
        }

        private static bool IsDead(UnitContext context)
        {
            return context.UnitEntity.IsDead;
        }
    }
}