﻿using Battle.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{
    public class Card : MonoBehaviour
    {
        public TextMeshProUGUI costText;
        public Image mainSprite;
        
        private CardConfig _config;

        public CardConfig Config
        {
            get => _config;
            set
            {
                _config = value;
                Refresh();
            }
        }

        private void Refresh()
        {
            if (_config == null)
                return;

            mainSprite.sprite = _config.mainSprite;
            costText.text = _config.cost.ToString();
        }
    }
}