using Source.Controller;
using Source.View;
using UnityEngine;
using Zenject;

namespace Source.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private MapView mapView;
        [SerializeField] private PlayerView playerView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MapView>().FromInstance(mapView).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerView>().FromInstance(playerView).AsSingle();

            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
        }
    }
}