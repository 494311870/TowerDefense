using Battle.Shared;
using UnityEngine;

namespace Battle.Unit.StateManagement
{
    public class UnitBehaviourContext
    {
        public UnitAgent UnitAgent;

        public IReadonlyUnitData UnitOriginalData;
        // public UnitEntity UnitEntity;


        public Transform DefaultTarget;
        public Transform CurrentTarget;


        public CircleTargetScanner EnemyScanner;
        public RectTargetScanner FriendScanner;

        public bool IsDead;

        public void ScanEnemy()
        {
            EnemyScanner.Scan(UnitAgent.Center);
        }
    }
}