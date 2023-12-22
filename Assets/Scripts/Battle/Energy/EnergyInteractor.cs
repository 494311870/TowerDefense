using UnityEngine;

namespace Battle.Energy
{
    [CreateAssetMenu(menuName = "Battle/Energy/EnergyInteractor")]
    public class EnergyInteractor : ScriptableObject
    {
        [SerializeField] private int efficiency;
        [SerializeField] private float productionInterval;

        private int _currentPoint;

        private float _timeSinceProduced;
        private EnergyPresenter _presenter;

        public int CurrentPoint => _currentPoint;

        public void ProvidePresenter(EnergyPresenter presenter)
        {
            _presenter = presenter;
            presenter.UpdatePoint(_currentPoint);
        }

        public void Produce(float deltaTime)
        {
            _timeSinceProduced += deltaTime;
            if (_timeSinceProduced >= productionInterval)
            {
                _timeSinceProduced -= productionInterval;
                ProduceEnergy();
            }
        }

        private void ProduceEnergy()
        {
            _currentPoint = CurrentPoint + efficiency;

            if (_presenter != null)
                _presenter.UpdatePoint(CurrentPoint);
        }

        public void RemovePresenter()
        {
            _presenter = null;
        }
    }
}