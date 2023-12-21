#region

using UnityEngine;

#endregion

namespace Battle.Card
{
    public class CardSlotsController : MonoBehaviour
    {
        public CardInteractor interactor;
        public CardSlots cardSlots;


        private void OnEnable()
        {
            cardSlots.UseCardEvent += CardSlotsOnUseCardEvent;
            interactor.SetFactionLayer(gameObject.layer);
        }

        private void OnDisable()
        {
            cardSlots.UseCardEvent -= CardSlotsOnUseCardEvent;
        }

        private void CardSlotsOnUseCardEvent(CardConfig config)
        {
            interactor.UseCard(config);
        }
    }
}