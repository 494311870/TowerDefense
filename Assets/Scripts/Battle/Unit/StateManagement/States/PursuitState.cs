using Battle.Shared;
using StateManagement;
using UnityEngine;

namespace Battle.Unit.StateManagement.States
{
    public class PursuitState : State<UnitBehaviourContext>
    {
        public override void Enter()
        {
            Collider2D attackTarget = Context.EnemyScanner.Target;
            if (attackTarget != null)
                Context.CurrentTarget = attackTarget.transform;
        }

        public override void Exit()
        {
            Context.CurrentTarget = Context.DefaultTarget;
        }

        public override void Update(float deltaTime)
        {
            Vector2 direction = GetMoveDirection();

            if (HasAnyFriendlyBlock(direction))
            {
                WaitingInPlace();
                return;
            }

            Move(direction);
        }

        private Vector2 GetMoveDirection()
        {
            Vector2 result = Context.CurrentTarget.position - Context.UnitAgent.transform.position;
            return result.normalized;
        }

        private bool HasAnyFriendlyBlock(Vector2 direction)
        {
            RectTargetScanner friendScanner = Context.FriendScanner;
            UnitAgent agent = Context.UnitAgent;

            Vector2 forwardOffset = friendScanner.ScanWidth * direction;
            friendScanner.Scan(agent.Center + forwardOffset);

            return friendScanner.Target != null;
        }

        private void WaitingInPlace()
        {
            Context.UnitAgent.InputHorizontal(0);
        }

        private void Move(Vector2 direction)
        {
            float horizontal = direction.x > 0 ? 1 : -1;
            Context.UnitAgent.InputHorizontal(horizontal);
        }
    }
}