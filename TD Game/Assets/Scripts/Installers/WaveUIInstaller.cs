using TDGame.UI;
using UnityEngine;
using Zenject;
namespace TDGame.Installers
{
    public class WaveUIInstaller : MonoInstaller
    {
        [SerializeField] private WaveUI _waveUI;
        public override void InstallBindings()
        {
            Container.Bind<WaveUI>()
                .FromInstance(_waveUI)
                .AsSingle()
                .NonLazy();
        }
    }
}

