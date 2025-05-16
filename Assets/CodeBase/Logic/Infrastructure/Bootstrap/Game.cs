using System;
using CodeBase.Logic.Formulas.Business;
using CodeBase.Logic.Infrastructure.Container;
using CodeBase.Logic.Providers.Data.Balance;
using CodeBase.Logic.Providers.Data.Saves;
using CodeBase.Logic.Providers.Data.ScriptableObjects;
using CodeBase.Logic.Services.Addressable;
using CodeBase.Logic.Services.Disposer;
using CodeBase.Logic.Services.ECS;
using CodeBase.Logic.Services.SaveLoad;
using CodeBase.Logic.Services.Update;
using CodeBase.Logic.Systems;
using CodeBase.Logic.Systems.Businesses.Buffs;
using CodeBase.Logic.Systems.Businesses.Payout;
using CodeBase.Logic.Systems.Businesses.Upgrade;
using CodeBase.Logic.Systems.SaveLoad;
using CodeBase.UI.Elements.Business.Factory;
using CodeBase.UI.Elements.Business.Provider;
using CodeBase.UI.Windows.Main;
using CodeBase.UI.Windows.Main.Factory;

namespace CodeBase.Logic.Infrastructure.Bootstrap
{
    public class Game
    {
        public void Launch()
        {
            IServiceLocator serviceLocator = new ServiceLocator();
            
            serviceLocator.Register(new DisposerService(serviceLocator));
            serviceLocator.Register<IUpdateService, IDisposable>(new UpdateService());
            serviceLocator.Register<IEcsService>(new EcsService());
            serviceLocator.Register<ISaveLoadService>(new SaveLoadService());
            serviceLocator.Register<IAddressableService>(new AddressableService());
            
            serviceLocator.Register<IBusinessFormulas>(new BusinessFormulas(serviceLocator));
            
            serviceLocator.Register<IPlayerProgressDataProvider>(new PlayerProgressDataProvider(serviceLocator));
            serviceLocator.Register<IBalanceDataProvider>(new BalanceDataProvider());
            serviceLocator.Register<IBusinessesSettingsProvider>(new BusinessesSettingsProvider(serviceLocator));
            serviceLocator.Register<IBusinessEntityProvider>(new BusinessEntityProvider(serviceLocator));
            
            serviceLocator.Register<IBusinessUpgradeSystem>(new BusinessUpgradeSystem(serviceLocator));
            serviceLocator.Register<IBusinessBuffSystem>(new BusinessBuffSystem(serviceLocator));
            serviceLocator.Register<IDisposable>(new BusinessPayoutSystem(serviceLocator));
            
            serviceLocator.Register<IBusinessFactory>(new BusinessFactory(serviceLocator));
            serviceLocator.Register<IMainWindowFactory>(new MainWindowFactory(serviceLocator));
            
            serviceLocator.Register<IDisposable>(new PlayerProgressDataSaver(serviceLocator));
            serviceLocator.Register<IPlayerProgressDataLoader>(new PlayerProgressDataLoader(serviceLocator));
            serviceLocator.Register<IMainWindow, IDisposable>(new MainWindow(serviceLocator));
            serviceLocator.Register(new MainSceneLoader(serviceLocator));
        }
    }
}