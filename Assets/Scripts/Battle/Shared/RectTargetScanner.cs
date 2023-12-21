using UnityEngine;

namespace Battle.Shared
{
    public class RectTargetScanner : TargetScanner
    {
        public float ScanHeight { get; set; }
        public float ScanWidth { get; set; }

        protected override int CheckCollider(Vector2 center, ContactFilter2D filter)
        {
            return Physics2D.OverlapBox(center, new Vector2(ScanWidth, ScanHeight), 0, filter, ColliderBuffer);
        }
    }
}