using TDGame.Money;
using Zenject;

namespace TDGame.Installers
{
    public class CurrencyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Currency>().FromComponentInHierarchy().AsSingle();
        }
    }
}

