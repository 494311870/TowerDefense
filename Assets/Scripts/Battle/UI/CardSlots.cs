using System;
using System.Collections.Generic;
using Battle.Config;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{
    public class CardSlots : MonoBehaviour
    {
        public CardSlot template;
        public Transform container;
        public LayoutGroup layoutGroup;
        public event Action<CardConfig> UseCardEvent;

        private List<CardSlot> _cardSlots;

        private void OnDestroy()
        {
            _cardSlots?.ForEach(x => x.UseCardEvent -= OnUseCardEvent);
        }

        public void Refresh(List<CardConfig> cardConfigs)
        {
            _cardSlots ??= new List<CardSlot>();
            _cardSlots.ForEach(x => x.gameObject.SetActive(false));

            for (int index = 0; index < cardConfigs.Count; index++)
            {
                CardConfig cardConfig = cardConfigs[index];
                CardSlot cardSlot = GetCardSlot(index);
                cardSlot.Refresh(cardConfig);
            }

            LayoutRebuilder.MarkLayoutForRebuild(layoutGroup.transform as RectTransform);
        }

        private CardSlot GetCardSlot(int index)
        {
            CardSlot cardSlot;
            if (index >= _cardSlots.Count)
            {
                cardSlot = Instantiate(template, container);
                cardSlot.UseCardEvent += OnUseCardEvent;
                _cardSlots.Add(cardSlot);
            }
            else
            {
                cardSlot = _cardSlots[index];
            }

            return cardSlot;
        }

        private void OnUseCardEvent(Card card)
        {
            UseCardEvent?.Invoke(card.Config);
        }
    }
}