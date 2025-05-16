using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Data.Static.Constants;
using CodeBase.Data.Static.Enums;
using CodeBase.Logic.Infrastructure;
using CodeBase.Logic.Infrastructure.Container;
using CodeBase.Logic.Providers.Data.Balance;
using CodeBase.Logic.Services.Addressable;
using CodeBase.UI.Elements.Business;
using CodeBase.UI.Elements.Business.Factory;
using CodeBase.UI.Windows.Main.Components;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Windows.Main.Factory
{
    public class MainWindowFactory : IMainWindowFactory
    {
        private readonly IAddressableService _addressableService;
        private readonly IBusinessFactory _businessFactory;
        private readonly IBalanceDataProvider _balanceDataProvider;

        private static readonly BusinessType[] _activeBusinessTypes = 
        {
            BusinessType.Business_1,
            BusinessType.Business_2,
            BusinessType.Business_3,
            BusinessType.Business_4,
            BusinessType.Business_5
        };
        
        public MainWindowFactory(IServiceLocator serviceLocator)
        {
            _balanceDataProvider = serviceLocator.Get<IBalanceDataProvider>();
            _businessFactory = serviceLocator.Get<IBusinessFactory>();
            _addressableService = serviceLocator.Get<IAddressableService>();
        }
        
        public async Task<MainWindowReferences> SpawnAsync()
        {
            GameObject prefab = await _addressableService.LoadAssetAsync<GameObject>(AddressableConstants.MainWindow);
            MainWindowView mainWindowView = Object.Instantiate(prefab).GetComponent<MainWindowView>();
            
            var balanceDisplay = new BalanceDisplay(mainWindowView.Balance, _balanceDataProvider);
            var businesses = await SpawnBusinessesAsync(mainWindowView.BusinessParent);
            
            var references = new MainWindowReferences()
            {
                View = mainWindowView,
                BalanceDisplay = balanceDisplay,
                Businesses = businesses,
            };
            
            return references;
        }

        private async Task<List<BusinessReferences>> SpawnBusinessesAsync(Transform parent)
        {
            var businesses = new List<BusinessReferences>(_activeBusinessTypes.Length);

            foreach (BusinessType businessType in _activeBusinessTypes)
            {
                var businessReferences = await _businessFactory.SpawnAsync(businessType, parent);
                businesses.Add(businessReferences);
            }
            
            return businesses;
        }
    }
}