using StateManagement;
using UnityEngine;

namespace Battle.Unit.Shared.StateManagement.States
{
    public class IdleState : State<UnitContext>
    {
        public override void Enter()
        {
            Debug.Log("Context == null");
            Context.UnitAgent.WaitingInPlace();
            Context.UnitAgent.ResetAttackCombo();
            Context.UnitEntity.CancelAttackCoolDown();
        }

        public override void Update(float deltaTime)
        {
            Context.ScanEnemy();
        }
    }
}