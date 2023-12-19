using Battle.Shared;
using UnityEngine;

namespace Battle.Unit.Shared.StateManagement
{
    public class UnitBehaviourContext
    {
        public Transform CurrentTarget;

        public CircleTargetScanner EnemyScanner;
        public RectTargetScanner FriendScanner;

        public Transform MarchTarget;
        public UnitAgent UnitAgent;
        public UnitEntity UnitEntity;

        public IReadonlyUnitData UnitOriginalData;

        public void ScanEnemy()
        {
            EnemyScanner.Scan(UnitAgent.Center);
        }
    }
}