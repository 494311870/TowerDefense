#region

using Battle.Unit;
using UnityEngine;

#endregion

namespace Battle.Card
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