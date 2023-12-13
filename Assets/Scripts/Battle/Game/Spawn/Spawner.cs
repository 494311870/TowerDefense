using UnityEngine;

namespace Battle.Game.Spawn
{
    public class Spawner : MonoBehaviour
    {
        public Transform moveTarget;
        public Transform container;

        public string friendLayer;
        public string enemyLayer;


        private void Spawn(GameObject prefab)
        {
            GameObject instance = Instantiate(prefab, container);
            
            ResetTransform(instance.transform);
            ResetUnit(instance);
        }

        private void ResetUnit(GameObject instance)
        {
            instance.layer = LayerMask.NameToLayer(friendLayer);
            if (instance.TryGetComponent(out UnitBehaviour unitBehaviour))
            {
                unitBehaviour.SetMoveTarget(moveTarget);
                unitBehaviour.checkLayerMask = LayerMask.GetMask(enemyLayer);
            }
        }

        private static void ResetTransform(Transform transform)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}