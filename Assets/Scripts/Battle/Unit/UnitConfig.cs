#region

using UnityEngine;

#endregion

namespace Battle.Unit
{
    [CreateAssetMenu(menuName = "Battle/Config/Unit")]
    public class UnitConfig : ScriptableObject
    {
        public GameObject prefab;
        public UnitData unitData;
    }
}