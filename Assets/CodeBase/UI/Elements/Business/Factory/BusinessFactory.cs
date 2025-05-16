using System.Threading.Tasks;
using CodeBase.Data.Static.Constants;
using CodeBase.Data.Static.Enums;
using CodeBase.Data.Static.Models;
using CodeBase.Logic.Infrastructure;
using CodeBase.Logic.Infrastructure.Container;
using CodeBase.Logic.Providers.Data.ScriptableObjects;
using CodeBase.Logic.Services.Addressable;
using CodeBase.Logic.Services.ECS;
using CodeBase.Logic.Systems.Businesses;
using CodeBase.Logic.Systems.Businesses.Upgrade;
using CodeBase.UI.Elements.Business.Components;
using CodeBase.UI.Elements.Business.Provider;
using Leopotam.EcsLite;
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
        private readonly IServiceLocator _serviceLocator;

        public BusinessFactory(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
            _businessesSettingsProvider = serviceLocator.Get<IBusinessesSettingsProvider>();
            _businessUpgradeSystem = serviceLocator.Get<IBusinessUpgradeSystem>();
            _ecsService = serviceLocator.Get<IEcsService>();
            _businessEntityProvider = serviceLocator.Get<IBusinessEntityProvider>();
            _addressableService = serviceLocator.Get<IAddressableService>();
        }
        
        public async Task<BusinessReferences> SpawnAsync(BusinessType businessType, Transform parent)
        {
            GameObject prefab = await _addressableService.LoadAssetAsync<GameObject>(AddressableConstants.Business);
            BusinessView businessView = Object.Instantiate(prefab, parent).GetComponent<BusinessView>();
            
            string name = await _businessesSettingsProvider.GetBusinessesNameAsync(businessType);
            
            BusinessParametersConfig parameters = await _businessesSettingsProvider
                .GetBusinessesParametersAsync(businessType);
            
            BusinessImprovementConfig improvements = await _businessesSettingsProvider
                .GetBusinessesImprovementsAsync(businessType);
            
            businessView.Name.text = name;
            
            EcsPackedEntity ecsPackedEntity = _businessEntityProvider.Provide(parameters.StartLevel,
                parameters.Income, parameters.Cost, parameters.DelayedIncomeTime, businessType);

            var firstImprovementButton = new BusinessIncomeBuffButton(businessView.FirstImprovementButton,
                businessView.FirstImprovementText, improvements.FirstImprovement, ecsPackedEntity, _serviceLocator);
            
            var secondImprovementButton = new BusinessIncomeBuffButton(businessView.SecondImprovementButton,
                businessView.SecondImprovementText, improvements.SecondImprovement, ecsPackedEntity, _serviceLocator);
            
            var levelUpgradeButton = new BusinessLevelUpgradeButton(businessView.LevelUpgradeButton,
                businessView.LevelUpgradeText, _ecsService, ecsPackedEntity, _businessUpgradeSystem);
            
            var payoutDisplay = new BusinessPayoutDisplay(businessView.IncomeProgressBar, _ecsService, ecsPackedEntity);
            var incomeDisplay = new BusinessIncomeDisplay(businessView.IncomeDisplay, _ecsService, ecsPackedEntity);
            var levelDisplay = new BusinessLevelDisplay(businessView.LevelDisplay, _ecsService, ecsPackedEntity);
            
            var references = new BusinessReferences()
            {
                View = businessView,
                FirstImprovementButton = firstImprovementButton,
                SecondImprovementButton = secondImprovementButton,
                LevelUpgradeButton = levelUpgradeButton,
                PayoutDisplay = payoutDisplay,
                IncomeDisplay = incomeDisplay,
                LevelDisplay = levelDisplay,
            };
            
            return references;
        }
    }
}