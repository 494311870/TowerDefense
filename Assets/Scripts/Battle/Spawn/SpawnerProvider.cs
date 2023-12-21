#region

using Battle.Shared;
using UnityEngine;

#endregion

namespace Battle.Spawn
{
    public class SpawnerProvider : MonoBehaviour
    {
        public Spawner spawner;
        public BattleSession battleSession;


        private void OnEnable()
        {
            battleSession.ProvideSpawner(spawner, spawner.gameObject.layer);
        }
    }
}