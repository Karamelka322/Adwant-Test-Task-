using System.Threading.Tasks;
using CodeBase.Data.Static.Constants;
using CodeBase.Data.Static.Enums;
using CodeBase.Data.Static.Models;
using CodeBase.Logic.Providers.Data.Balance;
using CodeBase.Logic.Providers.Data.ScriptableObjects;
using CodeBase.Logic.Services.Addressable;
using CodeBase.Logic.Services.Disposer;
using CodeBase.Logic.Services.ECS;
using CodeBase.Logic.Systems.Businesses;
using CodeBase.UI.Elements.Business.Provider;
using UnityEngine;

namespace CodeBase.UI.Elements.Business.Factory
{
    public class BusinessFactory : IBusinessFactory
    {
        private readonly IAddressableService _addressableService;
        private readonly IBusinessEntityProvider _businessEntityProvider;
        private readonly IEcsService _ecsService;
        private readonly IBusinessUpgradeSystem _businessUpgradeSystem;
        private readonly IBusinessesSettingsProvider _businessesSettingsProvider;
        private readonly IBalanceDataProvider _balanceDataProvider;
        private readonly IBusinessBuffSystem _businessBuffSystem;
        private readonly IDisposerService _disposerService;

        public BusinessFactory(
            IAddressableService addressableService,
            IBusinessUpgradeSystem businessUpgradeSystem,
            IBusinessEntityProvider businessEntityProvider,
            IBusinessesSettingsProvider businessesSettingsProvider,
            IEcsService ecsService,
            IBalanceDataProvider balanceDataProvider,
            IBusinessBuffSystem businessBuffSystem,
            IDisposerService disposerService)
        {
            _disposerService = disposerService;
            _businessBuffSystem = businessBuffSystem;
            _balanceDataProvider = balanceDataProvider;
            _businessesSettingsProvider = businessesSettingsProvider;
            _businessUpgradeSystem = businessUpgradeSystem;
            _ecsService = ecsService;
            _businessEntityProvider = businessEntityProvider;
            _addressableService = addressableService;
        }
        
        public async Task SpawnAsync(BusinessType businessType, Transform parent)
        {
            var prefab = await _addressableService.LoadAssetAsync<GameObject>(AddressableConstants.Business);
            var businessView = Object.Instantiate(prefab, parent).GetComponent<BusinessView>();
            
            string name = await _businessesSettingsProvider.GetBusinessesNameAsync(businessType);
            
            BusinessParametersConfig parameters = await _businessesSettingsProvider
                .GetBusinessesParametersAsync(businessType);
            
            BusinessImprovementConfig improvements = await _businessesSettingsProvider
                .GetBusinessesImprovementsAsync(businessType);
            
            businessView.Name.text = name;
            
            var ecsPackedEntity = _businessEntityProvider.Provide(parameters.StartLevel,
                parameters.Income, parameters.Cost, parameters.DelayedIncomeTime, businessType);

            new BusinessIncomeBuffButton(businessView.FirstImprovementButton, businessView.FirstImprovementText,
                improvements.FirstImprovement.Name, improvements.FirstImprovement.Id, improvements.FirstImprovement.Cost,
                improvements.FirstImprovement.Multiplier, _ecsService, ecsPackedEntity,
                _businessBuffSystem, _balanceDataProvider, _disposerService);

            new BusinessIncomeBuffButton(businessView.SecondImprovementButton, businessView.SecondImprovementText,
                improvements.SecondImprovement.Name, improvements.SecondImprovement.Id, improvements.SecondImprovement.Cost,
                improvements.SecondImprovement.Multiplier, _ecsService, ecsPackedEntity,
                _businessBuffSystem, _balanceDataProvider, _disposerService);
            
            new BusinessLevelUpgradeButton(businessView.LevelUpgradeButton, businessView.LevelUpgradeText,
                _ecsService, ecsPackedEntity, _businessUpgradeSystem, _disposerService);
            
            new BusinessPayoutDisplay(businessView.IncomeProgressBar, _ecsService, ecsPackedEntity, _disposerService);
            new BusinessIncomeDisplay(businessView.IncomeDisplay, _ecsService, ecsPackedEntity, _disposerService);
            new BusinessLevelDisplay(businessView.LevelDisplay, _ecsService, ecsPackedEntity, _disposerService);
        }
    }
}