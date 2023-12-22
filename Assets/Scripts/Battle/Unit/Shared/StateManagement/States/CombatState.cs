using UnityEngine;

namespace Battle.Unit.Shared.StateManagement.States
{
    public class CombatState : MoveState
    {
        public override void Enter()
        {
            base.Enter();
            Context.SelectEnemyAsTarget();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            
            if (Context.TargetIsInvalid)
            {
                Context.ScanEnemy();
                Context.SelectEnemyAsTarget();
            }
        
            // if (!Context.EnemyScanner.IsDetected)
            //     Context.ClearCurrentTarget();
        }
    }
}