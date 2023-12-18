using UnityEngine;

namespace Battle.Shared
{
    public class CircleTargetScanner : TargetScanner
    {
        protected override int CheckCollider(Vector2 center, ContactFilter2D filter)
        {
            return Physics2D.OverlapCircle(center, ScanRange, filter, ColliderBuffer);
        }

        public float ScanRange { get; set; }
    }
}