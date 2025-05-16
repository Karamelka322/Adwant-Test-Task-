using CodeBase.Data.Runtime.ECS.Components.Parameters;
using CodeBase.Logic.Formulas.Business;
using CodeBase.Logic.Infrastructure.Container;
using CodeBase.Logic.Providers.Data.Balance;
using CodeBase.Logic.Services.ECS;
using Leopotam.EcsLite;

namespace CodeBase.Logic.Systems.Businesses.Upgrade
{
    public class BusinessUpgradeSystem : IBusinessUpgradeSystem
    {
        private readonly IBusinessFormulas _businessFormulas;
        
        private readonly EcsPool<LevelParameters> _levelParametersPool;
        private readonly EcsPool<IncomeParameters> _incomeParametersPool;
        private readonly EcsPool<CostParameter> _costParametersPool;
        private readonly IBalanceDataProvider _balanceDataProvider;

        public BusinessUpgradeSystem(IServiceLocator serviceLocator)
        {
            _balanceDataProvider = serviceLocator.Get<IBalanceDataProvider>();
            _businessFormulas = serviceLocator.Get<IBusinessFormulas>();
            
            IEcsService ecsService = serviceLocator.Get<IEcsService>();
            
            _levelParametersPool = ecsService.GetPool<LevelParameters>();
            _incomeParametersPool = ecsService.GetPool<IncomeParameters>();
            _costParametersPool = ecsService.GetPool<CostParameter>();
        }
        
        public bool TryUpgradeLevel(int entity)
        {
            ref LevelParameters businessParameters = ref _levelParametersPool.Get(entity);

            if (_balanceDataProvider.Has(businessParameters.UpgradeCost.Value) == false)
            {
                return false;
            }
            
            SetLevel(entity, businessParameters.Level.Value + 1);
            _balanceDataProvider.Take(businessParameters.UpgradeCost.Value);
            
            return true;
        }

        public void SetLevel(int entity, int level)
        {
            ref LevelParameters businessParameters = ref _levelParametersPool.Get(entity);
            businessParameters.Level.Value = level;
            
            int businessCost = _costParametersPool.Get(entity).Cost.Value;
            
            businessParameters.UpgradeCost.Value = _businessFormulas.GetUpgradeLevelCost(
                businessParameters.Level.Value, businessCost);
            
            ref IncomeParameters incomeParameters = ref _incomeParametersPool.Get(entity);
            incomeParameters.CurrentIncome.Value = _businessFormulas.GetIncome(entity);
        }
    }
}