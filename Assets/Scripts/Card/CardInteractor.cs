using System.Collections.Generic;
using Battle;
using Battle.View.Spawn;
using Card.Config;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(menuName = "Card/CardInteractor")]
    public class CardInteractor : ScriptableObject
    {
        [SerializeField] private BattleSession battleSession;
        [SerializeField] private List<CardConfig> debugCardConfigs;
        private CardSlotsPresenter _presenter;
        private int _ownerId;

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
            (Spawner spawner, bool ok) = battleSession.GetSpawner(_ownerId);
            Debug.Log($"UseCard {ok}");
            if (!ok)
                return;
            
            spawner.Spawn(config.UnitConfig);
        }

        public void SetOwnerId(int ownerId)
        {
            _ownerId = ownerId;
        }
    }
}