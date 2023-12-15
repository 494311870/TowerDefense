using System;
using Card.Config;
using UnityEngine;
using UnityEngine.UI;

namespace Card.View
{
    public class CardSlot : MonoBehaviour
    {
        public CardRenderer cardRenderer;
        public Button useButton;
        
        public event Action<CardRenderer> UseCardEvent;


        private void OnEnable()
        {
            useButton.onClick.AddListener(OnUseButtonClicked);
        }

        private void OnDisable()
        {
            useButton.onClick.RemoveListener(OnUseButtonClicked);            
        }
        
        
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