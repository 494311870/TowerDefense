using System;
using UnityEngine;

namespace Battle.Projectile
{
    public class ProjectileAgent : MonoBehaviour
    {
        private Vector3 _velocity;
        public Vector2 Center => transform.position;

        public event Action DeathEvent;

        private void Update()
        {
            this.transform.position += _velocity * Time.deltaTime;
        }

        public void InputVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }

        public void WaitingInPlace()
        {
            InputVelocity(Vector2.zero);
        }

        public void Death()
        {
            DeathEvent?.Invoke();
        }
    }
}