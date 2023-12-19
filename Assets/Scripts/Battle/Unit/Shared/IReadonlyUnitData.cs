namespace Battle.Unit.Shared
{
    public interface IReadonlyUnitData
    {
        int Health { get; }
        int MoveSpeed { get; }
        int ThreatRange { get; }
        int AttackDamage { get; }
        int AttackSpeed { get; }
        int AttackRange { get; }
        int FriendSpace { get; }
    }
}