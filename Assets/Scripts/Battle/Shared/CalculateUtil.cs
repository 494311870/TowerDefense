#region

using UnityEngine;

#endregion

namespace Battle.Shared
{
    public static class CalculateUtil
    {
        public static float ConvertToWorldDistance(int value)
        {
            return 1.0f * value / 100;
        }

        public static float CalculateDistance(Vector2 center, Collider2D target)
        {
            return Vector2.Distance(center, target.transform.position);
        }

        public static float ConvertToAttackInterval(int attackSpeed)
        {
            float attackCount = 1.0f * attackSpeed / 100;
            return 1.0f / attackCount;
        }
    }
}