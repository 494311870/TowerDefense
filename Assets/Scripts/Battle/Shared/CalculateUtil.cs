#region

using UnityEngine;

#endregion

namespace Battle.Shared
{
    public static class CalculateUtil
    {
        public static float ConvertDistance(int distance)
        {
            return 1.0f * distance / 100;
        }

        public static float CalculateDistance(Vector2 center, Collider2D target)
        {
            return Vector2.Distance(center, target.transform.position);
        }

        public static float ConvertSpeed(int speed)
        {
            return 1.0f * speed / 100;
        }

        public static float ConvertAttackInterval(int attackSpeed)
        {
            float speed = 1.0f * attackSpeed / 100;
            return 1.0f / speed;
        }
    }
}