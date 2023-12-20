using StateManagement;

namespace Battle.Unit.Shared.StateManagement.States
{
    public class IdleState : State<UnitContext>
    {
        public override void Enter()
        {
            Context.UnitAgent.WaitingInPlace();
        }

        public override void Update(float deltaTime)
        {
            Context.ScanEnemy();
        }
    }
}