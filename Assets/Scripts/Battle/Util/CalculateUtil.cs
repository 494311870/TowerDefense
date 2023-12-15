using UnityEngine;

namespace Battle.Util
{
    public static class CalculateUtil
    {
        public static float ConvertDistance(int distance)
        {
            return  1.0f * distance / 100;
        }

        public static float CalculateDistance(Vector2 center, Collider2D target)
        {
            return Vector2.Distance(center, target.transform.position);
        }
    }
}