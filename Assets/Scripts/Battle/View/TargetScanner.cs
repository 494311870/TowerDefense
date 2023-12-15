#region

using System.Collections.Generic;
using Battle.Util;
using UnityEngine;

#endregion

namespace Battle.View
{
    public class TargetScanner
    {
        private readonly Collider2D[] _colliderBuffer;
        private int _colliderCount;
        private HashSet<Collider2D> _ignoredSet;

        public TargetScanner()
        {
            _colliderBuffer = new Collider2D[5];
        }

        public float ScanRange { get; set; }

        public Collider2D Target { get; private set; }

        public LayerMask LayerMask { get; set; }

        public void Scan(Vector2 center)
        {
            var filter = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = LayerMask
            };

            _colliderCount = Physics2D.OverlapCircle(center, ScanRange, filter, _colliderBuffer);

            if (CurrentTargetInRange())
                return;

            Target = GetNearestTarget(center);
        }

        private bool CurrentTargetInRange()
        {
            if (Target == null)
                return false;

            for (int index = 0; index < _colliderCount; index++)
            {
                if (_colliderBuffer[index] == Target)
                    return true;
            }

            return false;
        }

        private Collider2D GetNearestTarget(Vector2 center)
        {
            Collider2D result = null;
            float minDistance = ScanRange;
            // 找到距离最近的
            for (int index = 0; index < _colliderCount; index++)
            {
                Collider2D collider = _colliderBuffer[index];
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