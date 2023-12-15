using UnityEngine;

namespace Battle.Config
{
    [CreateAssetMenu(menuName = "Battle/Config/Unit")]
    public class UnitConfig : ScriptableObject
    {
        public GameObject prefab;
        public UnitData unitData;
    }
}