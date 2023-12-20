using UnityEngine;

namespace Battle.Projectile
{
    public class ProjectileContext
    {
        public ProjectileAgent ProjectileAgent { get; set; }
        public Vector2 StartPosition { get; set; }
        public Vector2 EndPosition { get; set; }


        public void ScanCollision()
        {
        }
    }
}