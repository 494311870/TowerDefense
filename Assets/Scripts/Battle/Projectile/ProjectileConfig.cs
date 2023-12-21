using UnityEngine;

namespace Battle.Projectile
{
    [CreateAssetMenu(menuName = "Battle/Projectile/Config")]
    public class ProjectileConfig : ScriptableObject
    {
        public GameObject prefab;
        public ProjectileData projectileData;
    }
}