#region

using UnityEngine;

#endregion

namespace Battle.Unit.Shared
{
    [CreateAssetMenu(menuName = "Battle/Unit/Config")]
    public class UnitConfig : ScriptableObject
    {
        public GameObject prefab;
        public UnitData unitData;
    }
}