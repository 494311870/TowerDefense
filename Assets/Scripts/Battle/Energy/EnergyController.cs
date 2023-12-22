using System;
using UnityEngine;

namespace Battle.Energy
{
    public class EnergyController : MonoBehaviour
    {
        public EnergyInteractor interactor;

        private void FixedUpdate()
        {
            interactor.Produce(Time.fixedDeltaTime);
        }
    }
}