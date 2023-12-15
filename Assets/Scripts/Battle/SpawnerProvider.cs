using System;
using Battle.View.Spawn;
using UnityEngine;

namespace Battle
{
    public class SpawnerProvider : MonoBehaviour
    {
        public int ownerId;
        public Spawner spawner;
        public BattleSession battleSession;


        private void OnEnable()
        {
            battleSession.ProvideSpawner(spawner,ownerId);
        }
    }
}