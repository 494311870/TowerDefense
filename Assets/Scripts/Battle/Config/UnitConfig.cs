using UnityEngine;

namespace Battle.Config
{
    [CreateAssetMenu(menuName = "Battle/Config/Unit")]
    public class UnitConfig : ScriptableObject
    {
        public GameObject prefab;
        public int health;
        public int moveSpeed;
        public int attackDamage;
        public int attackSpeed;
        public int attackRange;
    }
}