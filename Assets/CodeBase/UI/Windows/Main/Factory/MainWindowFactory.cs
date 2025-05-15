using System.Threading.Tasks;
using CodeBase.Data.Static.Constants;
using CodeBase.Data.Static.Enums;
using CodeBase.Logic.Providers.Data.Balance;
using CodeBase.Logic.Services.Addressable;
using CodeBase.Logic.Services.Disposer;
using CodeBase.UI.Elements.Balance;
using CodeBase.UI.Elements.Business.Factory;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Windows.Main.Factory
{
    public class MainWindowFactory : IMainWindowFactory
    {
        private readonly IAddressableService _addressableService;
        private readonly IBusinessFactory _businessFactory;
        private readonly IBalanceDataProvider _balanceDataProvider;
        private readonly IDisposerService _disposerService;

        private static readonly BusinessType[] _activeBusinessTypes = 
        {
            BusinessType.Business_1,
            BusinessType.Business_2,
            BusinessType.Business_3,
            BusinessType.Business_4,
            BusinessType.Business_5
        };
        
        public MainWindowFactory(
            IAddressableService addressableService,
            IBusinessFactory businessFactory, 
            IBalanceDataProvider balanceDataProvider,
            IDisposerService disposerService)
        {
            _disposerService = disposerService;
            _balanceDataProvider = balanceDataProvider;
            _businessFactory = businessFactory;
            _addressableService = addressableService;
        }
        
        public async Task<MainWindowView> SpawnAsync()
        {
            GameObject prefab = await _addressableService.LoadAssetAsync<GameObject>(AddressableConstants.MainWindow);
            MainWindowView mainWindowView = Object.Instantiate(prefab).GetComponent<MainWindowView>();
            
            new BalanceDisplay(mainWindowView.Balance, _balanceDataProvider, _disposerService);
            
            foreach (var businessType in _activeBusinessTypes)
            {
                await _businessFactory.SpawnAsync(businessType, mainWindowView.BusinessParent);
            }
            
            return mainWindowView;
        }
    }
}