#region

using System.Collections.Generic;
using Battle.Energy;
using Battle.Shared;
using Battle.Spawn;
using UnityEngine;

#endregion

namespace Battle.Card
{
    [CreateAssetMenu(menuName = "Card/CardInteractor")]
    public class CardInteractor : ScriptableObject
    {
        [SerializeField] private BattleSession battleSession;
        [SerializeField] private EnergyInteractor energyInteractor;
        [SerializeField] private List<CardConfig> debugCardConfigs;
        private int _factionLayer;
        private CardSlotsPresenter _presenter;

        private List<CardConfig> CardConfigs => debugCardConfigs;

        public void ProvidePresenter(CardSlotsPresenter presenter)
        {
            _presenter = presenter;
            _presenter.InitCards(CardConfigs);
        }

        public void RemovePresenter()
        {
            _presenter = null;
        }

        public void UseCard(CardConfig config)
        {
            if (config.cost > energyInteractor.CurrentPoint)
            {
                LogUseCard(false);
                return;
            }
            
            (Spawner spawner, bool ok) = battleSession.GetSpawner(_factionLayer);
            LogUseCard(ok);
            if (!ok)
                return;

            spawner.Spawn(config.UnitConfig);
        }

        private void LogUseCard(bool ok)
        {
            Debug.Log($"[{_factionLayer}]UseCard {(ok ? "Success" : "Fail")}");
        }

        public void SetFactionLayer(int factionLayer)
        {
            _factionLayer = factionLayer;
        }
    }
}