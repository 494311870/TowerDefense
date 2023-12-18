namespace Battle.Unit.StateManagement
{
    public class UnitEntity
    {
        public int Health { get; private set; }
        public int MoveSpeed { get; private set; }
        public int AttackRange { get; set; }
        

        public void Load(IReadonlyUnitData unitData)
        {
            Health = unitData.Health;
            MoveSpeed = unitData.MoveSpeed;
            AttackRange = unitData.AttackRange;
        }
    }
}