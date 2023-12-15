using System.Collections.Generic;
using Card.Config;
using Card.View;
using UnityEngine;

namespace Card
{
    public class CardSlotsPresenter : MonoBehaviour
    {
        public CardInteractor interactor;
        public CardSlots cardSlots;

        private void OnEnable()
        {
            interactor.ProvidePresenter(this);
        }

        private void OnDisable()
        {
            interactor.RemovePresenter();
        }

        public void InitCards(IReadOnlyList<CardConfig> cardConfigs)
        {
            cardSlots.Refresh(cardConfigs);
        }
    }
}