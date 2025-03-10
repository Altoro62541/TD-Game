using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PurchasePanelInstaller : MonoInstaller
{
    [SerializeField] private PurchasePanel _purchasePanel;

    public override void InstallBindings()
    {
            Container.Bind<PurchasePanel>()
                .FromInstance(_purchasePanel)
                .AsSingle()
                .NonLazy();
    }
}
