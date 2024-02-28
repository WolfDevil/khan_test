using Source.Configs;
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
        }
    }
}