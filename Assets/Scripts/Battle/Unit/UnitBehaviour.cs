#region

using Battle.Shared;
using Battle.Unit.StateManagement;
using StateManagement;
using UnityEngine;

#endregion

namespace Battle.Unit
{
    public class UnitBehaviour : MonoBehaviour, IAttackTarget
    {
        public UnitAgent agent;

        private int _currentHp;
        private bool _isDead;

        private StateMachine<UnitBehaviourContext> _stateMachine;
        private UnitBehaviourContext _context;

        private void Awake()
        {
            _context = new UnitBehaviourContext()
            {
                UnitAgent = agent,
                EnemyScanner = new CircleTargetScanner(),
                FriendScanner = new RectTargetScanner(),
            };
            _stateMachine = new UnitBehaviourStateMachine(_context);
        }

        private void FixedUpdate()
        {
            _stateMachine.Update(Time.fixedDeltaTime);
            _stateMachine.CheckTransitions();
        }

        public void Hurt(int damage)
        {
            if (_isDead)
                return;

            agent.Hurt(damage);

            _currentHp -= Mathf.Max(0, damage);
            if (_currentHp <= 0)
                Death();
        }

        /// <summary>
        /// 提供给动画的攻击命中事件
        /// </summary>
        private void OnAttackAnimationEvent()
        {
            _context.ScanEnemy();
            Collider2D target = _context.EnemyScanner.Target;
            if (target == null)
                return;

            if (target.TryGetComponent(out IAttackTarget attackTarget))
                attackTarget.Hurt(_context.UnitOriginalData.AttackDamage);
        }

        public void SetData(UnitData unitData)
        {
            _context.UnitOriginalData = unitData;
            _currentHp = unitData.Health;

            CircleTargetScanner enemyScanner = _context.EnemyScanner;
            RectTargetScanner friendScanner = _context.FriendScanner;
            
            enemyScanner.ScanRange = CalculateUtil.ConvertDistance(unitData.AttackRange);
            friendScanner.ScanWidth = CalculateUtil.ConvertDistance(unitData.FriendSpace);
            friendScanner.ScanHeight = agent.unitCollider.bounds.size.y;
            friendScanner.AddIgnored(agent.unitCollider);

            agent.SetData(unitData);
        }

        public void SetMoveTarget(Transform target)
        {
            _context.DefaultTarget = target;
            _context.CurrentTarget = target;
        }

        public void Death()
        {
            _isDead = true;
            agent.Death();
        }

        public void SetEnemyLayer(LayerMask layerMask)
        {
            _context.EnemyScanner.LayerMask = layerMask;
        }

        public void SetFriendLayer(LayerMask layerMask)
        {
            _context.FriendScanner.LayerMask = layerMask;
        }
    }
}