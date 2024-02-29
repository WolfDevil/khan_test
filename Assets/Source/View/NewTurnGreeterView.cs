using System;
using System.Threading.Tasks;
using Source.Signals;
using TMPro;
using UnityEngine;
using Zenject;

namespace Source.View
{
    public class NewTurnGreeterView : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;

        [SerializeField] private TextMeshProUGUI textMeshProUGUI;

        private void Start()
        {
            _signalBus.Subscribe<EndTurnSignal>(OnNewTurn);

            textMeshProUGUI.enabled = false;
        }

        private void OnNewTurn()
        {
            NewTurnTextShowAsync();
        }

        private async void NewTurnTextShowAsync()
        {
            textMeshProUGUI.enabled = true;

            var timer = 1f;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
            }

            textMeshProUGUI.enabled = false;
        }
    }
}