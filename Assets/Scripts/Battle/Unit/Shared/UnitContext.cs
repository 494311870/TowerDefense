using System;
using Battle.Faction;
using Battle.Shared;
using UnityEngine;

namespace Battle.Unit.Shared
{
    public class UnitContext : IDisposable
    {
        public UnitContext()
        {
            UnitEntity = new UnitEntity();
        }

        /// <summary>
        ///     阵营层级
        /// </summary>
        public int FactionLayer { get; set; }

        public ITarget CurrentTarget { get; set; }
        public CircleTargetScanner EnemyScanner { get; set; }
        public RectTargetScanner FriendScanner { get; set; }
        public ITarget MarchTarget { get; set; }
        public UnitAgent UnitAgent { get; set; }
        public UnitEntity UnitEntity { get; }
        public IReadonlyUnitData UnitOriginalData { get; private set; }
        public BattleSession BattleSession { get; set; }

        public void Dispose()
        {
            (FactionContext factionContext, bool ok) = BattleSession.GetFaction(FactionLayer);
            if (ok)
                factionContext.RemoveUnit(this);
        }

        public void ScanEnemy()
        {
            EnemyScanner.Scan(UnitAgent.Center);
        }

        public void SelectEnemyAsTarget()
        {
            if (!EnemyScanner.IsDetected)
            {
                CurrentTarget = null;
                return;
            }

            EnemyScanner.Target.TryGetComponent(out IAttackTarget attackTarget);
            CurrentTarget = attackTarget;
        }

        public void Load(IReadonlyUnitData unitData)
        {
            UnitOriginalData = unitData;
            UnitEntity.Load(unitData);
        }

        public void JoinBattle(BattleSession battleSession)
        {
            BattleSession = battleSession;
            (FactionContext factionContext, bool ok) = BattleSession.GetFaction(FactionLayer);
            if (!ok)
                return;

            MarchTarget = factionContext.Target;
        }

        public (FactionContext factionContext, bool ok) GetFaction()
        {
            return BattleSession.GetFaction(FactionLayer);
        }

        public void UpdateScannerLayer()
        {
            UpdateEnemyLayer();
            UpdateFriendLayer();
        }

        private void UpdateFriendLayer()
        {
            string layerName = LayerMask.LayerToName(FactionLayer);
            FriendScanner.LayerMask = LayerMask.GetMask(layerName);
        }

        private void UpdateEnemyLayer()
        {
            (FactionContext factionContext, bool ok) = GetFaction();
            if (ok)
            {
                string layerName = LayerMask.LayerToName(factionContext.Target.FactionLayer);
                EnemyScanner.LayerMask = LayerMask.GetMask(layerName);
            }
        }

        public void ClearCurrentTarget()
        {
            CurrentTarget = null;
        }
    }
}