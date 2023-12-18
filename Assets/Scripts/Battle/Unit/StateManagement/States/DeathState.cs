using StateManagement;

namespace Battle.Unit.StateManagement.States
{
    public class DeathState : State<UnitBehaviourContext>
    {
        public override void Enter()
        {
            Context.IsDead = true;
            Context.UnitAgent.WaitingInPlace();
            Context.UnitAgent.Death();
        }
    }
}