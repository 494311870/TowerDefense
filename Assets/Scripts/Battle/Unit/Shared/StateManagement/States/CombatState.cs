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
            Context.ScanEnemy();
            Context.SelectEnemyAsTarget();
            
            base.Update(deltaTime);
        }
    }
}