using UnityEngine;

namespace Battle.Shared
{
    public class CircleTargetScanner : TargetScanner
    {
        public float ScanRange { get; set; }

        protected override int CheckCollider(Vector2 center, ContactFilter2D filter)
        {
            return Physics2D.OverlapCircle(center, ScanRange, filter, ColliderBuffer);
        }
    }
}