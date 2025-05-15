using CodeBase.Logic.Formulas.Business;
using CodeBase.Logic.Providers.Data.Balance;
using CodeBase.Logic.Providers.Data.Saves;
using CodeBase.Logic.Providers.Data.ScriptableObjects;
using CodeBase.Logic.Services.Addressable;
using CodeBase.Logic.Services.Disposer;
using CodeBase.Logic.Services.ECS;
using CodeBase.Logic.Services.SaveLoad;
using CodeBase.Logic.Services.Update;
using CodeBase.Logic.Systems;
using CodeBase.Logic.Systems.Businesses;
using CodeBase.UI.Elements.Business.Factory;
using CodeBase.UI.Elements.Business.Provider;
using CodeBase.UI.Windows.Main;
using CodeBase.UI.Windows.Main.Factory;

namespace CodeBase.Logic.Infrastructure
{
    public class Game
    {
        public void Launch()
        {
            IDisposerService disposerService = new DisposerService();
            IUpdateService updateService = new UpdateService(disposerService);
            IEcsService ecsService = new EcsService();
            ISaveLoadService saveLoadService = new SaveLoadService();
            IAddressableService addressableService = new AddressableService();
            
            IPlayerProgressDataProvider playerSaveDataProvider = new PlayerProgressDataProvider(saveLoadService);
            IBalanceDataProvider balanceDataProvider = new BalanceDataProvider();
            
            IBusinessFormulas businessFormulas = new BusinessFormulas(ecsService);
            
            IBusinessUpgradeSystem businessUpgradeSystem = new BusinessUpgradeSystem(
                ecsService, businessFormulas, balanceDataProvider);
            
            IBusinessBuffSystem businessBuffSystem = new BusinessBuffSystem(ecsService, businessFormulas);
            
            BusinessPayoutSystem businessPayoutSystem = new BusinessPayoutSystem(ecsService, updateService,
                balanceDataProvider, disposerService);
            
            PlayerProgressDataSaver playerProgressDataSaver = new PlayerProgressDataSaver(disposerService,
                playerSaveDataProvider, balanceDataProvider, ecsService);
            
            IPlayerProgressDataLoader playerProgressDataLoader = new PlayerProgressDataLoader(playerSaveDataProvider,
                ecsService, businessBuffSystem, businessUpgradeSystem, balanceDataProvider);
            
            IBusinessesSettingsProvider businessesSettingsProvider = new BusinessesSettingsProvider(addressableService);
            IBusinessEntityProvider businessEntityProvider = new BusinessEntityProvider(ecsService, businessFormulas);
            
            IBusinessFactory businessFactory = new BusinessFactory(addressableService, businessUpgradeSystem, 
                businessEntityProvider, businessesSettingsProvider, ecsService, balanceDataProvider,
                businessBuffSystem, disposerService);
            
            IMainWindowFactory mainWindowFactory = new MainWindowFactory(addressableService
                , businessFactory, balanceDataProvider, disposerService);
            
            IMainWindow mainWindow = new MainWindow(mainWindowFactory);

            MainSceneLoader mainSceneLoader = new MainSceneLoader(mainWindow, playerProgressDataLoader);
        }
    }
}