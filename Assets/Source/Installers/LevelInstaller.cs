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
        [SerializeField] private HUDView hudView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MapView>().FromInstance(mapView).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerView>().FromInstance(playerView).AsSingle();
            Container.BindInterfacesAndSelfTo<HUDView>().FromInstance(hudView).AsSingle();

            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
        }
    }
}