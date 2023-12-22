using Battle.Shared;
using StateManagement;
using UnityEngine;
using static Battle.Shared.CalculateUtil;

namespace Battle.Unit.Shared.StateManagement.States
{
    public class AttackState : State<UnitContext>
    {

        public override void Enter()
        {
            Context.UnitAgent.WaitingInPlace();
        }

        public override void Update(float deltaTime)
        {
            Context.UnitEntity.CoolingAttack(deltaTime);
            TryAttack();
            
            // if (Context.TargetIsInvalid)
            // {
            //     Context.ScanEnemy();
            //     Context.SelectEnemyAsTarget();
            // }
            // else
            {
                LookAtTarget();
            }
        }

        private void LookAtTarget()
        {
            ITarget target = Context.Target;
            UnitAgent agent = Context.UnitAgent;

            agent.LookAt(target.Position);
        }

        private void TryAttack()
        {
            if (CanAttack())
                Attack();
        }

        private void Attack()
        {
            Context.UnitEntity.Attack();
            Context.UnitAgent.Attack();
        }

        private bool CanAttack()
        {
            return Context.UnitEntity.AttackIsReady;
        }
    }
}