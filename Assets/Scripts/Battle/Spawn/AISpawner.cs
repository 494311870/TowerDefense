#region

using System.Collections;
using Battle.Unit;
using Battle.Unit.Shared;
using UnityEngine;

#endregion

namespace Battle.Spawn
{
    public class AISpawner : Spawner
    {
        public float spawnDelay = 5.0f;
        public float spawnInterval = 5.0f;
        public UnitConfig unitConfig;

        private void Start()
        {
            StartCoroutine(AutoSpawn());
        }

        private IEnumerator AutoSpawn()
        {
            yield return new WaitForSeconds(spawnDelay);
            while (true)
            {
                Spawn(unitConfig);
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}