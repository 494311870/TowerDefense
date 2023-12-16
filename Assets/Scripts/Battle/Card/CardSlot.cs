#region

using System;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Battle.Card
{
    public class CardSlot : MonoBehaviour
    {
        public CardRenderer cardRenderer;
        public Button useButton;


        private void OnEnable()
        {
            useButton.onClick.AddListener(OnUseButtonClicked);
        }

        private void OnDisable()
        {
            useButton.onClick.RemoveListener(OnUseButtonClicked);
        }

        public event Action<CardRenderer> UseCardEvent;


        private void OnUseButtonClicked()
        {
            UseCardEvent?.Invoke(cardRenderer);
        }

        public void Refresh(CardConfig cardConfig)
        {
            cardRenderer.Config = cardConfig;
        }
    }
}