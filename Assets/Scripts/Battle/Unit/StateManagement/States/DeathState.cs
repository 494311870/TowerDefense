using StateManagement;

namespace Battle.Unit.StateManagement.States
{
    public class DeathState : State<UnitBehaviourContext>
    {
        public override void Enter()
        {
            Context.UnitAgent.WaitingInPlace();
            Context.UnitAgent.Death();
        }
    }
}