using System;
using UnityEngine;

namespace Battle.Projectile
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        public Action<ProjectileBehaviour> OnRecycle { get; set; }

        public void SetTarget(Vector3 position)
        {
            
        }
        
        

        private void Recycle()
        {
            OnRecycle?.Invoke(this);
        }
    }
}