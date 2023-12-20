using Battle.Shared;
using UnityEngine;

namespace Battle.Projectile
{
    public class ProjectileContext
    {
        public ProjectileAgent ProjectileAgent { get; set; }
        public ProjectileEntity ProjectileEntity { get; set; }
        public ProjectileData ProjectileData { get; set; }
        public Vector2 StartPosition { get; set; }
        public Vector2 EndPosition { get; set; }
        public CircleTargetScanner HitScanner { get; set; }
        public bool IsConsumed { get; set; }


        public void ScanCollision()
        {
            HitScanner.Scan(ProjectileAgent.Center);
        }
    }
}