using UnityEngine;

namespace Battle.Unit
{
    public class UnitEntity
    {
        public int Health { get; private set; }
        public int MoveSpeed { get; private set; }
        public int AttackRange { get; private set; }
        public int AttackSpeed { get; private set; }
        public int AttackDamage { get; private set; }

        public bool IsDead => Health <= 0;

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
    }
}