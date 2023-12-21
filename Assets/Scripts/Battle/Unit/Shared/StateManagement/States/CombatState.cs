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

            Context.ScanEnemy();

            // if (!Context.EnemyScanner.IsDetected)
            //     Context.ClearCurrentTarget();
        }
    }
}