using System;
using UnityEngine;

namespace Battle.UI
{
    public class BattleUIPresenter : MonoBehaviour
    {
        public BattleSession session;
        public CardSlots cardSlots;

        public void Start()
        {
            cardSlots.Refresh(session.CardConfigs);
        }
    }
}