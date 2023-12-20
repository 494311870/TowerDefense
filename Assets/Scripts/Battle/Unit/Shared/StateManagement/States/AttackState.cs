using StateManagement;
using UnityEngine;
using static Battle.Shared.CalculateUtil;

namespace Battle.Unit.Shared.StateManagement.States
{
    public class AttackState : State<UnitContext>
    {
        private float _attackCoolDown;

        public override void Enter()
        {
            _attackCoolDown = 0f;
            Context.UnitAgent.ResetAttackCombo();
            Context.UnitAgent.WaitingInPlace();
        }

        public override void Update(float deltaTime)
        {
            UpdateAttackCoolDown(deltaTime);
            LookAtTarget();
            TryAttack();
        }

        private void UpdateAttackCoolDown(float deltaTime)
        {
            if (_attackCoolDown >= 0)
                _attackCoolDown -= deltaTime;
        }

        private void LookAtTarget()
        {
            Collider2D target = Context.EnemyScanner.Target;
            UnitAgent agent = Context.UnitAgent;
            
            agent.LookAt(target.transform.position);
        }

        private void TryAttack()
        {
            if (CanAttack())
                Attack();
        }

        private void Attack()
        {
            int attackSpeed = Context.UnitEntity.AttackSpeed;
            float attackInterval = ConvertToAttackInterval(attackSpeed);
            _attackCoolDown = attackInterval;

            Context.UnitAgent.Attack();
        }

        private bool CanAttack()
        {
            return _attackCoolDown <= 0;
        }
    }
}