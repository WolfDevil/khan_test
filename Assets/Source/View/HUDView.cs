using Source.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.View
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI energyText;
        [SerializeField] private Button endTurnButton;

        private SignalBus _signalBus;

        public void Setup(SignalBus signalBus, int energyLeft)
        {
            _signalBus = signalBus;
            energyText.text = $"Energy: {energyLeft}";

            _signalBus.Subscribe<EnergyChangedSignal>(OnEnergyChanged);

            endTurnButton.onClick.RemoveAllListeners();
            endTurnButton.onClick.AddListener(OnEndTurnClick);
        }

        private void OnEnergyChanged(EnergyChangedSignal signal)
        {
            energyText.text = $"Energy: {signal.Value}";
        }

        private void OnEndTurnClick()
        {
            _signalBus.TryFire<EndTurnSignal>();
        }
    }
}