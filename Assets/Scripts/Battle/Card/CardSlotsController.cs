#region

using UnityEngine;

#endregion

namespace Battle.Card
{
    public class CardSlotsController : MonoBehaviour
    {
        public int ownerId;
        public CardInteractor interactor;
        public CardSlots cardSlots;


        private void OnEnable()
        {
            cardSlots.UseCardEvent += CardSlotsOnUseCardEvent;
            interactor.SetOwnerId(ownerId);
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