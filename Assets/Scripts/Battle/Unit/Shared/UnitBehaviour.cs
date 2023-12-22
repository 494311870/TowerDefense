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

        private void Awake()
        {
            OnInit();
        }

        private void FixedUpdate()
        {
            _stateMachine.Update(Time.fixedDeltaTime);
        }

        bool ITarget.IsInvalid => Context.UnitEntity.IsDead;
        int ITarget.FactionLayer => Context.FactionLayer;
        Vector2 ITarget.Position => agent.Position;
        Vector2 ITarget.Center => agent.Center;
        GameObject ITarget.GameObject => agent.gameObject;

        void IAttackTarget.Hurt(int damage)
        {
            if (Context.UnitEntity.IsDead)
                return;

            agent.Hurt(damage);

            Context.UnitEntity.Hurt(damage);
        }

        protected virtual void OnInit()
        {
        }

        private void InitStateMachine(UnitContext unitContext)
        {
            _stateMachine = new StateMachine<UnitContext>(unitContext);
            _stateMachine.LogState = true;
            IStateMachineBuilder<UnitContext> stateMachineBuilder = GetStateMachineBuilder();
            stateMachineBuilder.BuildStates(_stateMachine);
            stateMachineBuilder.BuildTransitions(_stateMachine);
            _stateMachine.ResetState();
        }

        protected virtual IStateMachineBuilder<UnitContext> GetStateMachineBuilder()
        {
            return new UnitStateMachineBuilder();
        }

        public void BindContext(UnitContext unitContext)
        {
            Context = unitContext;
            InitScanner(unitContext);
            InitStateMachine(unitContext);
            OnBindContext(unitContext);
        }

        private void InitScanner(UnitContext unitContext)
        {
            var enemyScanner = new CircleTargetScanner();
            var friendScanner = new RectTargetScanner();

            unitContext.EnemyScanner = enemyScanner;
            unitContext.FriendScanner = friendScanner;
            unitContext.UpdateScannerLayer();

            IReadonlyUnitData unitData = unitContext.UnitOriginalData;
            enemyScanner.ScanRange = ConvertToWorldDistance(unitData.ThreatRange);
            friendScanner.ScanWidth = ConvertToWorldDistance(unitData.FriendSpace);
            friendScanner.ScanHeight = agent.unitCollider.bounds.size.y;
            friendScanner.AddIgnored(agent.unitCollider);
        }

        /// <summary>
        ///     提供给动画的攻击命中事件
        /// </summary>
        private void OnAttackAnimationEvent()
        {
            Debug.Log("OnAttackAnimationEvent");
            AttackDetection();
        }

        protected abstract void AttackDetection();

        protected virtual void OnBindContext(UnitContext session)
        {
        }
    }
}