using UnityEngine;

namespace Battle.Unit.StateManagement.States
{
    public class CombatState : MoveState
    {
        public override void Enter()
        {
            base.Enter();
            Context.CurrentTarget = Context.EnemyScanner.Target.transform;
        }
    }
}