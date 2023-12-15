using Battle.Config;
using UnityEngine;

namespace Card.Config
{
    [CreateAssetMenu(menuName = "Battle/Config/Card")]
    public class CardConfig : ScriptableObject
    {
        [SerializeField] private UnitConfig unitConfig;
        public Sprite mainSprite;
        public int cost;
        
        public UnitConfig UnitConfig => unitConfig;
    }
}