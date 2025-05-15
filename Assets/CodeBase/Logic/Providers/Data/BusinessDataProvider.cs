using CodeBase.Data.Static.Enums;
using CodeBase.Data.Static.ScriptableObjects;
using CodeBase.Logic.Providers.Data.Saves;
using CodeBase.Logic.Providers.Data.ScriptableObjects;

namespace CodeBase.Logic.Providers.Data
{
    public class BusinessDataProvider
    {
        private readonly IPlayerProgressDataProvider _playerSaveDataProvider;
        private readonly IBusinessesSettingsProvider _businessesSettingsProvider;
        
        private static readonly BusinessType[] _activeBusinessTypes = new[]
        {
            BusinessType.Business_1,
            BusinessType.Business_2,
            BusinessType.Business_3,
            BusinessType.Business_4,
            BusinessType.Business_5
        };
        
        public BusinessDataProvider(
            IPlayerProgressDataProvider playerSaveDataProvider, 
            IBusinessesSettingsProvider businessesSettingsProvider)
        {
            _businessesSettingsProvider = businessesSettingsProvider;
            _playerSaveDataProvider = playerSaveDataProvider;
        }
        
        public BusinessType[] GetActiveBusinessesTypes()
        {
            return _activeBusinessTypes;
        }
        
        public void GetLevel(BusinessType businessType)
        {
            
        }
        
        public void GetIncome(BusinessType businessType)
        {
            
        }
    }
}