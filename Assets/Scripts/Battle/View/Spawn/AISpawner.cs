using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Config;
using UnityEngine;

namespace Battle.View.Spawn
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
            yield return  new WaitForSeconds(spawnDelay);
            while (true)
            {
                yield return new WaitForSeconds(spawnInterval);                
                Spawn(unitConfig);                    
            }
        }
    }
}