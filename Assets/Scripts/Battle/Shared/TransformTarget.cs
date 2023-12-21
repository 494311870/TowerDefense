using UnityEngine;

namespace Battle.Shared
{
    public class TransformTarget : ITarget
    {
        private readonly Collider2D _collider;
        private readonly Transform _transform;

        public TransformTarget(Transform transform)
        {
            _transform = transform;
            if (transform.TryGetComponent(out Collider2D collider)) _collider = collider;
        }

        public bool IsInvalid => _transform == null;
        public int FactionLayer => _transform.gameObject.layer;
        public Vector2 Position => _transform.position;

        public Vector2 Center
        {
            get
            {
                if (_collider)
                    return _collider.bounds.center;
                return _transform.position;
            }
        }

        public GameObject GameObject => _transform.gameObject;
    }
}