#region

using Battle.Shared;
using Battle.Unit.Shared.StateManagement;
using StateManagement;
using UnityEngine;

#endregion

namespace Battle.Unit.Shared
{
    public abstract class UnitBehaviour : MonoBehaviour, IAttackTarget
    {
        public UnitAgent agent;

        private StateMachine<UnitBehaviourContext> _stateMachine;
        protected UnitBehaviourContext Context { get; private set; }

        private void Awake()
        {
            Context = new UnitBehaviourContext
            {
                UnitAgent = agent,
                UnitEntity = new UnitEntity(),
                EnemyScanner = new CircleTargetScanner(),
                FriendScanner = new RectTargetScanner()
            };
            _stateMachine = new UnitBehaviourStateMachine(Context);
            OnInit();
        }

        private void FixedUpdate()
        {
            _stateMachine.Update(Time.fixedDeltaTime);
        }

        public void Hurt(int damage)
        {
            if (Context.UnitEntity.IsDead)
                return;

            agent.Hurt(damage);

            Context.UnitEntity.Hurt(damage);
        }

        protected virtual void OnInit()
        {
        }

        public void ProvideData(IReadonlyUnitData unitData)
        {
            Context.UnitOriginalData = unitData;
            Context.UnitEntity.Load(unitData);

            CircleTargetScanner enemyScanner = Context.EnemyScanner;
            RectTargetScanner friendScanner = Context.FriendScanner;

            enemyScanner.ScanRange = CalculateUtil.ConvertToWorldDistance(unitData.ThreatRange);
            friendScanner.ScanWidth = CalculateUtil.ConvertToWorldDistance(unitData.FriendSpace);
            friendScanner.ScanHeight = agent.unitCollider.bounds.size.y;
            friendScanner.AddIgnored(agent.unitCollider);

            OnProvideData(unitData);
        }

        protected virtual void OnProvideData(IReadonlyUnitData unitData)
        {
        }

        public void SetMoveTarget(Transform target)
        {
            Context.MarchTarget = target;
        }

        public void SetEnemyLayer(LayerMask layerMask)
        {
            Context.EnemyScanner.LayerMask = layerMask;
        }

        public void SetFriendLayer(LayerMask layerMask)
        {
            Context.FriendScanner.LayerMask = layerMask;
        }

        /// <summary>
        /// 提供给动画的攻击命中事件
        /// </summary>
        private void OnAttackAnimationEvent()
        {
            AttackDetection();
        }

        protected abstract void AttackDetection();
    }
}