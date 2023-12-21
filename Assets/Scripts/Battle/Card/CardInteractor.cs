#region

using System.Collections.Generic;
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
            (Spawner spawner, bool ok) = battleSession.GetSpawner(_factionLayer);
            Debug.Log($"[{_factionLayer}]UseCard {(ok ? "Success" : "Fail")}");
            if (!ok)
                return;

            spawner.Spawn(config.UnitConfig);
        }

        public void SetFactionLayer(int factionLayer)
        {
            _factionLayer = factionLayer;
        }
    }
}