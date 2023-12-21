using UnityEngine;

namespace Battle.Shared
{
    public interface IAttackTarget
    {
        void Hurt(int damage);

        Vector2 Center { get; }
    }
}