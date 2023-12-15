using Battle.Config;
using UnityEngine;

namespace Battle.View.Spawn
{
    public class Spawner : MonoBehaviour
    {
        public Transform moveTarget;
        public Transform container;

        public string friendLayer;
        public string enemyLayer;

        public void Spawn(UnitConfig unitConfig)
        {
            GameObject instance = Instantiate(unitConfig.prefab, container);
            
            ResetTransform(instance.transform);
            ResetUnit(instance,unitConfig);
        }

     
        private void ResetUnit(GameObject instance, UnitConfig unitConfig)
        {
            instance.layer = LayerMask.NameToLayer(friendLayer);
            if (instance.TryGetComponent(out UnitBehaviour unitBehaviour))
            {
                unitBehaviour.SetData(unitConfig.unitData);
                unitBehaviour.SetMoveTarget(moveTarget);
                unitBehaviour.SetEnemyLayer(LayerMask.GetMask(enemyLayer));
                unitBehaviour.SetFriendLayer(LayerMask.GetMask(friendLayer));
            }
        }

        private static void ResetTransform(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}