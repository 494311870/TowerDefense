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
        public BattleSession BattleSession { get; set; }

        public void ScanEnemy()
        {
            EnemyScanner.Scan(UnitAgent.Center);
        }

        public void SelectEnemyAsTarget()
        {
            Collider2D target = this.EnemyScanner.Target;
            this.CurrentTarget = target ? target.transform : null;
        }
    }
}