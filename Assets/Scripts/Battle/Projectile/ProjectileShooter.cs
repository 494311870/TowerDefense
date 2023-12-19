﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Battle.Projectile
{
    public class ProjectileShooter : MonoBehaviour
    {
        public ProjectileConfig projectileConfig;

        private Transform _container;
        private Queue<ProjectileBehaviour> _projectilePool;

        private void Awake()
        {
            _projectilePool = new Queue<ProjectileBehaviour>();
        }

        public void ProvideContainer(Transform container)
        {
            _container = container;
        }

        public void Shoot(Vector3 position)
        {
            if (!_projectilePool.TryDequeue(out ProjectileBehaviour projectileBehaviour))
            {
                GameObject instance = Instantiate(projectileConfig.prefab, _container);
                if (instance.TryGetComponent(out projectileBehaviour))
                {
                    projectileBehaviour.OnRecycle = OnProjectileRecycle;
                }
            }


            projectileBehaviour.SetTarget(position);

        }

        private void OnProjectileRecycle(ProjectileBehaviour projectile)
        {
            _projectilePool.Enqueue(projectile);
        }

        private static void ResetTransform(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}