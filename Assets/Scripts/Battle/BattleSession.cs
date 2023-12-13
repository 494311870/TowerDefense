using System;
using System.Collections.Generic;
using Battle.Config;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(menuName = "Battle/Session")]
    public class BattleSession : ScriptableObject
    {
        [SerializeField] private List<CardConfig> debugCardConfigs;
        
        public List<CardConfig> CardConfigs => debugCardConfigs;        

    }
}