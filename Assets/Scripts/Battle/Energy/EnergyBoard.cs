using TMPro;
using UnityEngine;

namespace Battle.Energy
{
    public class EnergyBoard : MonoBehaviour
    {
        public TextMeshProUGUI pointText;

        public void SetPoint(string point)
        {
            pointText.text = point;
        }
    }
}