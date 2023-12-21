using Battle.Faction;
using Battle.Shared;
using UnityEngine;

namespace Battle
{
    public class BattleController : MonoBehaviour
    {
        public BattleSession battleSession;
        public Transform playerBaseCamp;
        public Transform computerBaseCamp;


        private void Start()
        {
            var playerFaction = new FactionData
            {
                BaseCamp = playerBaseCamp.ToTarget()
            };
            var computerFaction = new FactionData
            {
                BaseCamp = computerBaseCamp.ToTarget()
            };

            battleSession.Start(playerFaction, computerFaction);
        }

        private void OnDestroy()
        {
            battleSession.Close();
        }
    }
}