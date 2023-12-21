#region

using System.Collections.Generic;
using Battle.Faction;
using Battle.Spawn;
using UnityEngine;

#endregion

namespace Battle.Shared
{
    [CreateAssetMenu(fileName = "BattleSession", menuName = "Battle/BattleSession")]
    public class BattleSession : ScriptableObject
    {
        private Dictionary<int, FactionContext> _factionContextMap;

        private Dictionary<int, Spawner> _spawnerMap;
        public Transform ProjectileContainer { get; set; }

        public void ProvideSpawner(Spawner spawner, int factionLayer)
        {
            _spawnerMap ??= new Dictionary<int, Spawner>();
            _spawnerMap[factionLayer] = spawner;
        }

        public (Spawner spawner, bool ok) GetSpawner(int factionLayer)
        {
            if (_spawnerMap == null)
                return (null, false);

            bool ok = _spawnerMap.TryGetValue(factionLayer, out Spawner spawner);
            return (spawner, ok);
        }

        public (FactionContext factionContext, bool ok) GetFaction(int factionLayer)
        {
            if (_factionContextMap == null)
                return (null, false);

            bool ok = _factionContextMap.TryGetValue(factionLayer, out FactionContext factionContext);
            return (factionContext, ok);
        }

        public void Start(FactionData factionA, FactionData factionB)
        {
            _factionContextMap ??= new Dictionary<int, FactionContext>();

            var factionContextA = new FactionContext
            {
                Target = factionB.BaseCamp,
                Layer = factionA.BaseCamp.FactionLayer
            };

            var factionContextB = new FactionContext
            {
                Target = factionA.BaseCamp,
                Layer = factionB.BaseCamp.FactionLayer
            };

            _factionContextMap[factionContextA.Layer] = factionContextA;
            _factionContextMap[factionContextB.Layer] = factionContextB;
        }

        public void Close()
        {
            _spawnerMap?.Clear();
            _factionContextMap?.Clear();
        }
    }
}