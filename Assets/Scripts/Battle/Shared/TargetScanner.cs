#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Battle.Shared
{
    public abstract class TargetScanner
    {
        protected readonly Collider2D[] ColliderBuffer;
        private int _colliderCount;
        private HashSet<Collider2D> _ignoredSet;

        protected TargetScanner()
        {
            ColliderBuffer = new Collider2D[5];
        }

        public Collider2D Target { get; private set; }

        public LayerMask LayerMask { get; set; }

        public void Scan(Vector2 center)
        {
            var filter = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = LayerMask
            };

            _colliderCount = CheckCollider(center, filter);

            if (CurrentTargetInRange())
                return;

            Target = GetNearestTarget(center);
        }

        protected abstract int CheckCollider(Vector2 center, ContactFilter2D filter);

        private bool CurrentTargetInRange()
        {
            if (Target == null)
                return false;

            for (int index = 0; index < _colliderCount; index++)
            {
                if (ColliderBuffer[index] == Target)
                    return true;
            }

            return false;
        }

        private Collider2D GetNearestTarget(Vector2 center)
        {
            Collider2D result = null;
            float minDistance = float.MaxValue;
            // 找到距离最近的
            for (int index = 0; index < _colliderCount; index++)
            {
                Collider2D collider = ColliderBuffer[index];
                if (IsIgnored(collider))
                    continue;


                if (result == null)
                {
                    result = collider;
                }
                else
                {
                    float distance = CalculateUtil.CalculateDistance(center, collider);
                    if (!(distance < minDistance))
                        continue;

                    result = collider;
                    minDistance = distance;
                }
            }

            return result;
        }

        public void AddIgnored(Collider2D ignored)
        {
            _ignoredSet ??= new HashSet<Collider2D>();
            _ignoredSet.Add(ignored);
        }

        private bool IsIgnored(Collider2D collider)
        {
            if (_ignoredSet == null)
                return false;

            return _ignoredSet.Contains(collider);
        }
    }
}