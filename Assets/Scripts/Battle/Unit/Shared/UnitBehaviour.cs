#region

using Battle.Shared;
using Battle.Unit.Shared.StateManagement;
using StateManagement;
using UnityEngine;
using static Battle.Shared.CalculateUtil;

#endregion

namespace Battle.Unit.Shared
{
    public abstract class UnitBehaviour : MonoBehaviour, IAttackTarget
    {
        public UnitAgent agent;

        private StateMachine<UnitContext> _stateMachine;
        protected UnitContext Context { get; private set; }
        public BattleSession BattleSession { get; set; }

        private void Awake()
        {
            Context = new UnitContext
            {
                UnitAgent = agent,
                UnitEntity = new UnitEntity(),
                EnemyScanner = new CircleTargetScanner(),
                FriendScanner = new RectTargetScanner()
            };
            _stateMachine = new UnitStateMachine(Context);
            OnInit();
        }

        private void FixedUpdate()
        {
            _stateMachine.Update(Time.fixedDeltaTime);
        }

        public Vector2 Center => agent.Center;

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

            enemyScanner.ScanRange = ConvertToWorldDistance(unitData.ThreatRange);
            friendScanner.ScanWidth = ConvertToWorldDistance(unitData.FriendSpace);
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
            Debug.Log("OnAttackAnimationEvent");
            AttackDetection();
        }

        protected abstract void AttackDetection();

        public void ProvideSession(BattleSession session)
        {
            BattleSession = session;
            Context.BattleSession = session;
            OnProvideSession(session);
        }

        protected virtual void OnProvideSession(BattleSession session)
        {
        }

        public void ResetState()
        {
            _stateMachine.ResetState();
        }
    }
}