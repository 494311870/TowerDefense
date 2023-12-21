using System;
using UnityEngine;

namespace Battle.Projectile
{
    [Serializable]
    public class ProjectileData
    {
        [SerializeField] private int attackRange;
        [SerializeField] private float prepareTime;
        [SerializeField] private int moveSpeed;

        public int AttackRange
        {
            get => attackRange;
            set => attackRange = value;
        }

        public float PrepareTime
        {
            get => prepareTime;
            set => prepareTime = value;
        }

        public int MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }
    }
}