using System.Threading.Tasks;
using CodeBase.Data.Static.Enums;
using CodeBase.Data.Static.Models;

namespace CodeBase.Logic.Providers.Data.ScriptableObjects
{
    public interface IBusinessesSettingsProvider
    {
        Task<string> GetBusinessesNameAsync(BusinessType businessType);
        Task<BusinessParametersConfig> GetBusinessesParametersAsync(BusinessType businessType);
        Task<BusinessImprovementConfig> GetBusinessesImprovementsAsync(BusinessType businessType);
    }
}