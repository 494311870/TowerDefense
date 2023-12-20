using System;
using Battle.Projectile.StateManagement;
using UnityEngine;

namespace Battle.Projectile
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        private ProjectileStateMachine _stateMachine;
        private ProjectileContext _context;
        public Action<ProjectileBehaviour> OnRecycle { get; set; }

        private void Awake()
        {
            _context = new ProjectileContext();
            _stateMachine = new ProjectileStateMachine(_context);
        }

        private void Recycle()
        {
            OnRecycle?.Invoke(this);
        }

        public void SetStartPosition(Vector2 position)
        {
            _context.StartPosition = position;
        }

        public void SetEndPosition(Vector2 position)
        {
            _context.EndPosition = position;
        }

        public void ResetState()
        {
            _stateMachine.ResetState();
        }
    }
}