using System;
using Battle.Config;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{
    public class CardSlot : MonoBehaviour
    {
        public Card card;
        public Button useButton;
        
        public event Action<Card> UseCardEvent;


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
            UseCardEvent?.Invoke(card);
        }

        public void Refresh(CardConfig cardConfig)
        {
            card.Config = cardConfig;
        }
    }
}