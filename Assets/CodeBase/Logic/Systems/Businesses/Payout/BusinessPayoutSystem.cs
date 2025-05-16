using System;
using CodeBase.Data.Runtime.ECS.Components.Parameters;
using CodeBase.Data.Runtime.ECS.Components.Tags;
using CodeBase.Logic.Infrastructure.Container;
using CodeBase.Logic.Providers.Data.Balance;
using CodeBase.Logic.Services.ECS;
using CodeBase.Logic.Services.Update;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase.Logic.Systems.Businesses.Payout
{
    public class BusinessPayoutSystem : IDisposable
    {
        private readonly IUpdateService _updateService;
        private readonly IBalanceDataProvider _balanceDataProvider;
        
        private readonly EcsPool<IncomeParameters> _incomeParametersPool;
        private readonly EcsPool<LevelParameters> _levelParametersPool;
        private readonly EcsFilter _businessFilter;
        
        public BusinessPayoutSystem(IServiceLocator serviceLocator)
        {
            _balanceDataProvider = serviceLocator.Get<IBalanceDataProvider>();
            _updateService = serviceLocator.Get<IUpdateService>();
            
            IEcsService ecsService = serviceLocator.Get<IEcsService>();
            
            _incomeParametersPool = ecsService.GetPool<IncomeParameters>();
            _levelParametersPool = ecsService.GetPool<LevelParameters>();
            
            _businessFilter = ecsService.GetFilter<BusinessTag>().Inc<IncomeParameters>().Inc<LevelParameters>().End();
            
            _updateService.OnUpdate += UpdateHandler;
        }
        
        public void Dispose()
        {
            _updateService.OnUpdate -= UpdateHandler;
        }
        
        private void UpdateHandler()
        {
            foreach (int entity in _businessFilter)
            {
                LevelParameters levelParameters = _levelParametersPool.Get(entity);
                
                if (levelParameters.Level.Value == 0)
                {
                    continue;
                }
                
                ref IncomeParameters incomeParameters = ref _incomeParametersPool.Get(entity);

                incomeParameters.PayoutProgress.Value += Time.deltaTime / incomeParameters.PayoutDelay.Value;
                
                if (incomeParameters.PayoutProgress.Value >= 1f)
                {
                    incomeParameters.PayoutProgress.Value = 0;
                    _balanceDataProvider.Add(incomeParameters.CurrentIncome.Value);
                }
            }
        }
    }
}