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

        private StateMachine<UnitBehaviourContext> _stateMachine;
        private UnitBehaviourContext _context;

        private void Awake()
        {
            _context = new UnitBehaviourContext()
            {
                UnitAgent = agent,
                UnitEntity = new UnitEntity(),
                EnemyScanner = new CircleTargetScanner(),
                FriendScanner = new RectTargetScanner(),
            };
            _stateMachine = new UnitBehaviourStateMachine(_context);
        }

        private void FixedUpdate()
        {
            _stateMachine.Update(Time.fixedDeltaTime);
        }

        public void Hurt(int damage)
        {
            if (_context.UnitEntity.IsDead)
                return;

            agent.Hurt(damage);

            _context.UnitEntity.Hurt(damage);
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
                attackTarget.Hurt(_context.UnitEntity.AttackDamage);
        }

        public void SetData(IReadonlyUnitData unitData)
        {
            _context.UnitOriginalData = unitData;
            _context.UnitEntity.Load(unitData);

            CircleTargetScanner enemyScanner = _context.EnemyScanner;
            RectTargetScanner friendScanner = _context.FriendScanner;

            enemyScanner.ScanRange = CalculateUtil.ConvertToWorldDistance(unitData.ThreatRange);
            friendScanner.ScanWidth = CalculateUtil.ConvertToWorldDistance(unitData.FriendSpace);
            friendScanner.ScanHeight = agent.unitCollider.bounds.size.y;
            friendScanner.AddIgnored(agent.unitCollider);
        }

        public void SetMoveTarget(Transform target)
        {
            _context.MarchTarget = target;
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