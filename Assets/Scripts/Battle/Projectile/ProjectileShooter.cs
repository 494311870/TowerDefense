using UnityEngine;
using UnityEngine.Pool;

namespace Battle.Projectile
{
    public class ProjectileShooter : MonoBehaviour
    {
        public ProjectileConfig projectileConfig;

        private int _attackDamage;
        private Transform _container;
        private ObjectPool<ProjectileBehaviour> _projectilePool;
        private LayerMask _attackLayerMask;

        public void SetAttackLayerMask(LayerMask value)
        {
            _attackLayerMask = value;
        }

        private void Awake()
        {
            _projectilePool = new ObjectPool<ProjectileBehaviour>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                OnDestroyPoolObject);
        }

        private ProjectileBehaviour CreatePooledItem()
        {
            GameObject instance = Instantiate(projectileConfig.prefab, _container);
            if (instance.TryGetComponent(out ProjectileBehaviour projectileBehaviour))
            {
                projectileBehaviour.OnRecycle = OnProjectileRecycle;
            }

            return projectileBehaviour;
        }

        private void OnProjectileRecycle(ProjectileBehaviour behaviour)
        {
            _projectilePool.Release(behaviour);
        }

        private static void OnDestroyPoolObject(ProjectileBehaviour behaviour)
        {
            Destroy(behaviour.gameObject);
        }

        private static void OnReturnedToPool(ProjectileBehaviour behaviour)
        {
            behaviour.gameObject.SetActive(false);
        }

        private static void OnTakeFromPool(ProjectileBehaviour behaviour)
        {
            ResetTransform(behaviour.transform);
            behaviour.gameObject.SetActive(true);
        }

        private static void ResetTransform(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public void ProvideContainer(Transform container)
        {
            _container = container;
        }

        public void SetAttackDamage(int attackDamage)
        {
            _attackDamage = attackDamage;
        }

        public void Shoot(Vector2 from, Vector2 to)
        {
            ProjectileBehaviour projectileBehaviour = _projectilePool.Get();

            projectileBehaviour.ProvideData(projectileConfig.projectileData);
            projectileBehaviour.SetAttackDamage(_attackDamage);
            projectileBehaviour.SetAttackLayerMask(_attackLayerMask);
            projectileBehaviour.SetStartPosition(from);
            projectileBehaviour.SetEndPosition(to);
            projectileBehaviour.ResetState();
        }
    }
}