using System;
using Battle.Projectile.StateManagement;
using Battle.Shared;
using UnityEngine;
using static Battle.Shared.CalculateUtil;

namespace Battle.Projectile
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        public ProjectileAgent agent;

        private ProjectileStateMachine _stateMachine;
        private ProjectileContext _context;
        public Action<ProjectileBehaviour> OnRecycle { get; set; }

        private void Awake()
        {
            _context = new ProjectileContext()
            {
                ProjectileAgent = agent,
                HitScanner = new CircleTargetScanner(),
                ProjectileEntity = new ProjectileEntity()
            };
            _stateMachine = new ProjectileStateMachine(_context);
        }


        private void OnEnable()
        {
            agent.DeathEvent += Recycle;
        }

        private void OnDisable()
        {
            agent.DeathEvent -= Recycle;
        }

        private void FixedUpdate()
        {
            _stateMachine.Update(Time.fixedDeltaTime);
        }

        private void Recycle()
        {
            OnRecycle?.Invoke(this);
        }

        public void ProvideData(ProjectileData projectileData)
        {
            CircleTargetScanner hitScanner = _context.HitScanner;
            hitScanner.ScanRange = ConvertToWorldDistance(projectileData.AttackRange);
            _context.ProjectileData = projectileData;
            _context.ProjectileEntity.Load(projectileData);
        }


        public void SetStartPosition(Vector2 position)
        {
            _context.StartPosition = position;
        }

        public void SetEndPosition(Vector2 position)
        {
            _context.EndPosition = position;
        }

        public void ResetState()
        {
            _stateMachine.ResetState();
        }

        public void SetAttackDamage(int attackDamage)
        {
            _context.ProjectileEntity.AttackDamage = attackDamage;
        }
    }
}