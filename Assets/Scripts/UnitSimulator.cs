using System;
using Battle.Config;
using Battle.Util;
using Battle.View;
using UnityEngine;

public class UnitSimulator : MonoBehaviour
{
    public UnitConfig unitConfig;

    public UnitAgent unitAgent;

    private UnitConfig _currentConfig;

    private void Update()
    {
        if (!unitConfig || !unitAgent)
            return;

        if (_currentConfig != unitConfig)
        {
            _currentConfig = unitConfig;
            ReloadConfig();
        }
        else if (unitAgent.DataInvalid)
        {
            ReloadConfig();
        }
    }

    private void ReloadConfig()
    {
        unitAgent.SetData(unitConfig.unitData);
        unitAgent.unitRigidbody.isKinematic = true;
        unitAgent.unitRigidbody.velocity = Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        if (!unitConfig || !unitAgent)
            return;
        
        Gizmos.color = new Color(1, 0.5f, 0, 0.5f);
        float attackRange = CalculateUtil.ConvertDistance(unitConfig.unitData.AttackRange);
        Gizmos.DrawSphere(unitAgent.Center, attackRange);
    }
}