#region

using Battle.Faction;
using Battle.Shared;
using Battle.Unit.Shared;
using UnityEngine;

#endregion

namespace Battle.Spawn
{
    public class Spawner : MonoBehaviour
    {
        public BattleSession battleSession;

        // public Transform moveTarget;
        public Transform container;

        public void Spawn(UnitConfig unitConfig)
        {
            GameObject instance = Instantiate(unitConfig.prefab, container);

            ResetTransform(instance.transform);
            ResetUnit(instance, unitConfig);
        }

        private void ResetUnit(GameObject instance, UnitConfig unitConfig)
        {
            int factionLayer = gameObject.layer;
            instance.layer = factionLayer;

            (FactionContext factionContext, bool ok) = battleSession.GetFaction(factionLayer);
            if (!ok)
            {
                Debug.LogWarning($"Spawn Unit Failed: FactionContext Not Found [{factionLayer}]");
                return;
            }

            UnitContext unitContext = factionContext.CreateUnit();
            factionContext.AddUnit(unitContext);

            unitContext.JoinBattle(battleSession);
            unitContext.Load(unitConfig.unitData);

            if (instance.TryGetComponent(out UnitAgent agent))
                unitContext.UnitAgent = agent;

            if (instance.TryGetComponent(out UnitBehaviour unitBehaviour))
                unitBehaviour.BindContext(unitContext);
        }


        private static void ResetTransform(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}