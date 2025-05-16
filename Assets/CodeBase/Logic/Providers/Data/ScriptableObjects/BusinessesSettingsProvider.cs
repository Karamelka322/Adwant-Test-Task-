using System.Threading.Tasks;
using CodeBase.Data.Static.Constants;
using CodeBase.Data.Static.Enums;
using CodeBase.Data.Static.Models;
using CodeBase.Data.Static.ScriptableObjects;
using CodeBase.Logic.Infrastructure;
using CodeBase.Logic.Infrastructure.Container;
using CodeBase.Logic.Services.Addressable;

namespace CodeBase.Logic.Providers.Data.ScriptableObjects
{
    public class BusinessesSettingsProvider : IBusinessesSettingsProvider
    {
        private readonly IAddressableService _addressableService;

        public BusinessesSettingsProvider(IServiceLocator serviceLocator)
        {
            _addressableService = serviceLocator.Get<IAddressableService>();
        }
        
        public async Task<string> GetBusinessesNameAsync(BusinessType businessType)
        {
            BusinessesNameSettings settings = await _addressableService.LoadAssetAsync<BusinessesNameSettings>(
                AddressableConstants.BusinessesNamesSettings);

            foreach (BusinessNameConfig config in settings.Business)
            {
                if (config.Type == businessType)
                {
                    return config.Name;
                }
            }
            
            return string.Empty;
        }
        
        public async Task<BusinessParametersConfig> GetBusinessesParametersAsync(BusinessType businessType)
        {
            BusinessesParametersSettings settings = await _addressableService.LoadAssetAsync<BusinessesParametersSettings>(
                AddressableConstants.BusinessesParametersSettings);
            
            foreach (BusinessParametersConfig config in settings.Business)
            {
                if (config.Type == businessType)
                {
                    return config;
                }
            }
            
            return default;
        }

        public async Task<BusinessImprovementConfig> GetBusinessesImprovementsAsync(BusinessType businessType)
        {
            BusinessesImprovementSettings settings = await _addressableService.LoadAssetAsync<BusinessesImprovementSettings>(
                AddressableConstants.BusinessesImprovementsSettings);
            
            foreach (var config in settings.Business)
            {
                if (config.BusinessType == businessType)
                {
                    return config;
                }
            }
            
            return default;
        }
    }
}