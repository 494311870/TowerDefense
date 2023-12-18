using StateManagement;

namespace Battle.Unit.StateManagement.States
{
    public class IdleState : State<UnitBehaviourContext>
    {
        public override void Enter()
        {
            Context.UnitAgent.WaitingInPlace();
        }
    }
}