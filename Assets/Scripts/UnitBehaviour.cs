using System;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    public UnitAgent agent;
    [Range(0.1f, 10f)] public float attackRange = 1f;
    public LayerMask checkLayerMask;
    
    
    private Collider2D[] _colliderBuffer;
    private Collider2D _attackTarget;
    private Transform _moveTarget;

    private void Awake()
    {
        _colliderBuffer = new Collider2D[5];
    }

    public void SetMoveTarget(Transform target)
    {
        _moveTarget = target;
    }

    public void OnRecycle()
    {
        _attackTarget = null;
        _moveTarget = null;
    }

    private void Update()
    {
        UpdateAttackTarget();
        UpdateState();
    }

    private void UpdateState()
    {
        if (_attackTarget !=null)
        {
            UpdateAttackState();
        }
        else
        {
            UpdateMoveState();
        }
    }

    private void UpdateMoveState()
    {
        if (!_moveTarget)
            return;
        
        Vector3 dir = _moveTarget.position - this.transform.position;

        float horizontal = dir.x > 0 ? 1 : -1;
        agent.InputHorizontal(horizontal);        
    }

    private void UpdateAttackState()
    {
        if (agent.CanAttack())
        {
            agent.Attack();
            if (_attackTarget.TryGetComponent(out IAttackTarget attackTarget))
            {
                attackTarget.Hurt(agent.attackPower);
            }
        }
    }

    private void UpdateAttackTarget()
    {
        var filter = new ContactFilter2D
        {
            useLayerMask = true,
            layerMask = checkLayerMask
        };

        int count = Physics2D.OverlapCircle(this.transform.position, attackRange, filter, _colliderBuffer);

        if (CurrentTargetInRange(count)) 
            return;

        _attackTarget = GetNearestTarget(count);
    }

    private bool CurrentTargetInRange(int count)
    {
        if (_attackTarget == null) 
            return false;
        
        for (int index = 0; index < count; index++)
        {
            if (_colliderBuffer[index] == _attackTarget)
            {
                return true;
            }
        }

        return false;
    }

    private Collider2D GetNearestTarget(int count)
    {
        Collider2D result = null;
        float minDistance = attackRange;
        // 找到距离最近的
        for (int index = 0; index < count; index++)
        {
            Collider2D aCollider = _colliderBuffer[index];
            if (result == null)
            {
                result = aCollider;
            }
            else
            {
                float distance = CalculateDistance(aCollider);
                if (!(distance < minDistance)) 
                    continue;
                
                result = aCollider;
                minDistance = distance;
            }
        }

        return result;
    }

    private float CalculateDistance(Collider2D target)
    {
        return Vector2.Distance(this.transform.position, target.transform.position);        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0.5f, 0, 0.5f);
        Gizmos.DrawSphere(this.transform.position, attackRange);
    }
}