#region

using System;
using Battle.Shared;
using Battle.Unit;
using Battle.Unit.Shared;
using UnityEngine;

#endregion

namespace Battle.Spawn
{
    public class Spawner : MonoBehaviour
    {
        public BattleSession battleSession;
        public Transform moveTarget;
        public Transform container;

        public string friendLayer;
        public string enemyLayer;


        public void Spawn(UnitConfig unitConfig)
        {
            GameObject instance = Instantiate(unitConfig.prefab, container);

            ResetTransform(instance.transform);
            ResetUnit(instance, unitConfig);
        }


        private void ResetUnit(GameObject instance, UnitConfig unitConfig)
        {
            instance.layer = LayerMask.NameToLayer(friendLayer);
            if (instance.TryGetComponent(out UnitBehaviour unitBehaviour))
            {
                unitBehaviour.ProvideSession(battleSession);
                unitBehaviour.ProvideData(unitConfig.unitData);
                unitBehaviour.SetMoveTarget(moveTarget);
                unitBehaviour.SetEnemyLayer(LayerMask.GetMask(enemyLayer));
                unitBehaviour.SetFriendLayer(LayerMask.GetMask(friendLayer));
                unitBehaviour.ResetState();
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