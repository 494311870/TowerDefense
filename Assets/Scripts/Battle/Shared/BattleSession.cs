#region

using System.Collections.Generic;
using Battle.Spawn;
using UnityEngine;

#endregion

namespace Battle.Shared
{
    [CreateAssetMenu(fileName = "BattleSession", menuName = "Battle/BattleSession")]
    public class BattleSession : ScriptableObject
    {
        public Transform ProjectileContainer { get; set; }
        
        private Dictionary<int, Spawner> _spawnerMap;

        public void ProvideSpawner(Spawner spawner, int ownerId)
        {
            _spawnerMap ??= new Dictionary<int, Spawner>();
            _spawnerMap[ownerId] = spawner;
        }

        public (Spawner spawner, bool ok) GetSpawner(int ownerId)
        {
            if (_spawnerMap == null)
                return (null, false);

            bool ok = _spawnerMap.TryGetValue(ownerId, out Spawner spawner);
            return (spawner, ok);
        }

        public void Close()
        {
            _spawnerMap?.Clear();
        }
    }
}