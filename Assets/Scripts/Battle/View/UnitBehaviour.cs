using Battle.Config;
using Battle.Util;
using UnityEngine;

namespace Battle.View
{
    public class UnitBehaviour : MonoBehaviour, IAttackTarget
    {
        public UnitAgent agent;

        private Transform _moveTarget;
        private Vector2 _centerOffset;


        private int _currentHp;
        private bool _isDead;
        private UnitData _unitData;

        private TargetScanner _enemyScanner;
        private TargetScanner _friendScanner;

        private void Awake()
        {
            _enemyScanner = new TargetScanner();
            _friendScanner = new TargetScanner();
        }

        private void Update()
        {
            UpdateState();
        }

        private void UpdateState()
        {
            if (_isDead)
                return;

            _enemyScanner.Scan(agent.Center);
            if (_enemyScanner.Target != null)
            {
                TryAttack();
            }
            else
            {
                Vector2 direction = GetMoveDirection();
                Vector2 forwardOffset = _friendScanner.ScanRange * direction;
                _friendScanner.Scan(agent.Center + forwardOffset);
                // 移动方向有友军，暂停移动
                if (_friendScanner.Target != null)
                    return;

                MoveToTarget();
            }
        }

        private void MoveToTarget()
        {
            if (!_moveTarget)
                return;

            Vector2 dir = GetMoveDirection();

            float horizontal = dir.x > 0 ? 1 : -1;
            agent.InputHorizontal(horizontal);
        }

        private Vector2 GetMoveDirection()
        {
            Vector2 result = _moveTarget.position - this.transform.position;
            return result.normalized;
        }

        private void TryAttack()
        {
            if (agent.CanAttack())
                agent.Attack();
        }


        /// <summary>
        /// 提供给动画的攻击命中事件
        /// </summary>
        private void OnAttackAnimationEvent()
        {
            _enemyScanner.Scan(agent.Center);
            Collider2D target = _enemyScanner.Target;
            if (target == null)
                return;

            if (target.TryGetComponent(out IAttackTarget attackTarget))
            {
                attackTarget.Hurt(_unitData.AttackDamage);
            }
        }

        public void Hurt(int damage)
        {
            if (_isDead)
                return;

            _currentHp -= Mathf.Max(0, damage);

            if (_currentHp <= 0)
                Death();
        }

        public void SetData(UnitData unitData)
        {
            _unitData = unitData;
            _currentHp = _unitData.Health;
            _enemyScanner.ScanRange = CalculateUtil.ConvertDistance(_unitData.AttackRange);
            _friendScanner.ScanRange = CalculateUtil.ConvertDistance(_unitData.FriendSpace);
            _friendScanner.AddIgnored(agent.unitCollider);

            agent.SetData(_unitData);
        }

        public void SetMoveTarget(Transform target)
        {
            _moveTarget = target;
        }

        public void Death()
        {
            _isDead = true;
            agent.Death();
        }

        public void SetEnemyLayer(LayerMask layerMask)
        {
            _enemyScanner.LayerMask = layerMask;
        }

        public void SetFriendLayer(LayerMask layerMask)
        {
            _friendScanner.LayerMask = layerMask;
        }
    }
}