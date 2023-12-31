﻿using StateManagement;

namespace Battle.Unit.Shared.StateManagement.States
{
    public class DeathState : State<UnitContext>
    {
        public override void Enter()
        {
            Context.UnitAgent.WaitingInPlace();
            Context.UnitAgent.Death();
        }
    }
}