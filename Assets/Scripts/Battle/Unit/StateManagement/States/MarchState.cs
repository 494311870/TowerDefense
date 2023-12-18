using StateManagement;
using UnityEngine;

namespace Battle.Unit.StateManagement.States
{
    public class MarchState : MoveState
    {
        public override void Enter()
        {
            base.Enter();
            Context.CurrentTarget = Context.MarchTarget;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Context.ScanEnemy();
        }
    }
}