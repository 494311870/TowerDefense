#region

using Battle.Shared;
using UnityEngine;

#endregion

namespace Battle.Unit.Shared
{
    public class UnitSimulator : MonoBehaviour
    {
        public UnitConfig unitConfig;
        public UnitAgent unitAgent;

        private UnitAgent _currentUnitAgent;

        private void Update()
        {
            if (!unitConfig || !unitAgent)
                return;

            if (_currentUnitAgent != unitAgent)
            {
                _currentUnitAgent = unitAgent;
                ResetAgent();
            }
        }

        private void OnDrawGizmos()
        {
            if (!unitConfig || !unitAgent)
                return;

            Gizmos.color = new Color(1, 0.5f, 0, 0.5f);
            float distance = CalculateUtil.ConvertToWorldDistance(unitConfig.unitData.AttackRange);
            Gizmos.DrawSphere(unitAgent.Center, distance);
        }

        private void ResetAgent()
        {
            unitAgent.unitRigidbody.isKinematic = true;
            unitAgent.unitRigidbody.velocity = Vector2.zero;
        }
    }
}