using UnityEngine;

namespace Battle.Energy
{
    public class EnergyPresenter : MonoBehaviour
    {
        public EnergyInteractor interactor;
        public EnergyBoard energyBoard;

        private void OnEnable()
        {
            interactor.ProvidePresenter(this);
        }

        private void OnDisable()
        {
            interactor.RemovePresenter();
        }

        public void UpdatePoint(int point)
        {
            energyBoard.SetPoint(point.ToString());
        }
    }
}