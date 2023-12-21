namespace Battle.Shared
{
    public interface IAttackTarget : ITarget
    {
        void Hurt(int damage);
    }
}