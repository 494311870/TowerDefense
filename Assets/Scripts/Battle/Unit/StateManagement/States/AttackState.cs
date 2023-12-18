using StateManagement;
using UnityEngine;

namespace Battle.Unit.StateManagement.States
{
    public class AttackState : State<UnitBehaviourContext>
    {
        public override void Enter()
        {
            Context.UnitAgent.WaitingInPlace();
        }

        public override void Update(float deltaTime)
        {
            LookAtTarget();
            TryAttack();
        }

        private void LookAtTarget()
        {
            Collider2D target = Context.EnemyScanner.Target;
            UnitAgent agent = Context.UnitAgent;
            Vector3 direction = target.transform.position - agent.transform.position;
            agent.SwapSpriteToward(direction.x);
        }

        private void TryAttack()
        {
            UnitAgent agent = Context.UnitAgent;
            if (agent.CanAttack())
                agent.Attack();
        }
    }
}