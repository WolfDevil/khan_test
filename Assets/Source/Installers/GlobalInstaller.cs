using Source.Configs;
using Source.Signals;
using UnityEngine;
using Zenject;

namespace Source.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig gameConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameConfig>().FromInstance(gameConfig).AsSingle();
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<EnergyChangedSignal>();
        }
    }
}