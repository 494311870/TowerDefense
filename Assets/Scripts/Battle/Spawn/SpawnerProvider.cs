#region

using Battle.Shared;
using UnityEngine;

#endregion

namespace Battle.Spawn
{
    public class SpawnerProvider : MonoBehaviour
    {
        public int ownerId;
        public Spawner spawner;
        public BattleSession battleSession;


        private void OnEnable()
        {
            battleSession.ProvideSpawner(spawner, ownerId);
        }
    }
}