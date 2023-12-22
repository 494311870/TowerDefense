﻿using Battle.Shared;
using StateManagement;
using UnityEngine;
using static Battle.Shared.CalculateUtil;

namespace Battle.Unit.Shared.StateManagement.States
{
    public abstract class MoveState : State<UnitContext>
    {
        public override void Update(float deltaTime)
        {
            if (IsStationed())
                return;

            if (Context.TargetIsInvalid)
                return;

            Vector2 direction = GetMoveDirection();
            if (HasAnyFriendlyBlock(direction))
            {
                WaitingInPlace();
                return;
            }

            Move(direction);
        }

        private bool IsStationed()
        {
            return Context.UnitEntity.IsStationed;
        }

        protected Vector2 GetMoveDirection()
        {
            Vector2 result = Context.Target.Position - Context.UnitAgent.Position;
            return result.normalized;
        }

        protected bool HasAnyFriendlyBlock(Vector2 direction)
        {
            RectTargetScanner friendScanner = Context.FriendScanner;
            UnitAgent agent = Context.UnitAgent;

            Vector2 forwardOffset = friendScanner.ScanWidth * direction;
            friendScanner.Scan(agent.Center + forwardOffset);

            return friendScanner.IsDetected;
        }

        protected void WaitingInPlace()
        {
            Context.UnitAgent.WaitingInPlace();
        }

        protected void Move(Vector2 direction)
        {
            float horizontal = direction.x > 0 ? 1 : -1;
            float worldDistance = ConvertToWorldDistance(Context.UnitEntity.MoveSpeed);
            Context.UnitAgent.InputHorizontalVelocity(horizontal * worldDistance);
        }
    }
}