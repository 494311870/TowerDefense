#region

using System;
using UnityEngine;

#endregion

namespace Battle.Unit.Shared
{
    [Serializable]
    public class UnitData : IReadonlyUnitData
    {
        [SerializeField] private int health;
        [SerializeField] private int moveSpeed;
        [SerializeField] private int threatRange;
        [SerializeField] private int attackDamage;
        [SerializeField] private int attackSpeed;
        [SerializeField] private int attackRange;
        [SerializeField] private int friendSpace;

        public int Health
        {
            get => health;
            set => health = value;
        }

        public int MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }

        public int ThreatRange
        {
            get => threatRange;
            set => threatRange = value;
        }

        public int AttackDamage
        {
            get => attackDamage;
            set => attackDamage = value;
        }

        public int AttackSpeed
        {
            get => attackSpeed;
            set => attackSpeed = value;
        }

        public int AttackRange
        {
            get => attackRange;
            set => attackRange = value;
        }

        public int FriendSpace
        {
            get => friendSpace;
            set => friendSpace = value;
        }
    }
}