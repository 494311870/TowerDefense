using Battle.Shared;
using UnityEngine;

namespace Battle.Unit.Shared
{
    public class UnitContext
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

        public void SelectEnemyAsTarget()
        {
            this.CurrentTarget = this.EnemyScanner.Target.transform;
        }
    }
}