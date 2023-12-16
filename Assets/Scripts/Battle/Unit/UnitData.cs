#region

using System;
using UnityEngine;

#endregion

namespace Battle.Unit
{
    [Serializable]
    public class UnitData
    {
        [SerializeField] private int health;
        [SerializeField] private int moveSpeed;
        [SerializeField] private int attackDamage;
        [SerializeField] private int attackSpeed;
        [SerializeField] private int attackRange;
        [SerializeField] private int friendSpace;


        public int Health => health;
        public int MoveSpeed => moveSpeed;
        public int AttackDamage => attackDamage;
        public int AttackSpeed => attackSpeed;
        public int AttackRange => attackRange;
        public int FriendSpace => friendSpace;
    }
}