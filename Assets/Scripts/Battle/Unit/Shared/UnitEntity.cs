using UnityEngine;
using static Battle.Shared.CalculateUtil;

namespace Battle.Unit.Shared
{
    public class UnitEntity
    {
        private float _attackCoolDown;
        public int Health { get; private set; }
        public int MoveSpeed { get; private set; }
        public int AttackRange { get; private set; }
        public int AttackSpeed { get; private set; }
        public int AttackDamage { get; private set; }

        public bool IsDead => Health <= 0;
        public bool IsStationed => MoveSpeed <= 0;

        public void Load(IReadonlyUnitData unitData)
        {
            Health = unitData.Health;
            MoveSpeed = unitData.MoveSpeed;
            AttackRange = unitData.AttackRange;
            AttackSpeed = unitData.AttackSpeed;
            AttackDamage = unitData.AttackDamage;
        }

        public void Hurt(int damage)
        {
            Health -= Mathf.Max(0, damage);
        }

        public bool AttackIsReady => _attackCoolDown <= 0;
        
        public void CoolingAttack(float deltaTime)
        {
            if (_attackCoolDown >= 0)
                _attackCoolDown -= deltaTime;
        }

        public void Attack()
        {
            float attackInterval = ConvertToAttackInterval(AttackSpeed);
            _attackCoolDown = attackInterval;
        }

        public void CancelAttackCoolDown()
        {
            _attackCoolDown = 0;
        }
    }
}