#region

using Battle.Unit.Shared;
using UnityEngine;

#endregion

namespace Battle.Card
{
    [CreateAssetMenu(menuName = "Battle/Card/Config")]
    public class CardConfig : ScriptableObject
    {
        [SerializeField] private UnitConfig unitConfig;
        public Sprite mainSprite;
        public int cost;

        public UnitConfig UnitConfig => unitConfig;
    }
}