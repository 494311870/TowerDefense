namespace Battle.Game
{
    public interface IAttackTarget
    {
        void Hurt(int damage);
        void Death();
    }
}