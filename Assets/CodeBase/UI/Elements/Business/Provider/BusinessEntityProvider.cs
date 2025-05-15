using CodeBase.Data.Runtime.ECS.Components.Parameters;
using CodeBase.Data.Runtime.ECS.Components.Tags;
using CodeBase.Data.Static.Enums;
using CodeBase.Logic.Formulas.Business;
using CodeBase.Logic.Services.ECS;
using Leopotam.EcsLite;
using UniRx;

namespace CodeBase.UI.Elements.Business.Provider
{
    public class BusinessEntityProvider : IBusinessEntityProvider
    {
        private readonly IEcsService _ecsService;
        private readonly IBusinessFormulas _businessFormulas;

        public BusinessEntityProvider(IEcsService ecsService, IBusinessFormulas businessFormulas)
        {
            _businessFormulas = businessFormulas;
            _ecsService = ecsService;
        }
        
        public EcsPackedEntity Provide(int level, int income, int cost, int payoutDelay, BusinessType type)
        {
            var entity = _ecsService.AddEntity();
            
            ref var levelParameters = ref _ecsService.GetPool<LevelParameters>().Add(entity);
            levelParameters.Level = new IntReactiveProperty(level);
            
            var upgradeCost = _businessFormulas.GetUpgradeLevelCost(level, cost);
            levelParameters.UpgradeCost = new IntReactiveProperty(upgradeCost);
            
            ref var incomeParameters = ref _ecsService.GetPool<IncomeParameters>().Add(entity);
            incomeParameters.BaseIncome = new IntReactiveProperty(income);
            incomeParameters.CurrentIncome = new IntReactiveProperty(income);
            incomeParameters.PayoutDelay = new IntReactiveProperty(payoutDelay);
            incomeParameters.PayoutProgress = new FloatReactiveProperty();
            
            ref var costParameters = ref _ecsService.GetPool<CostParameter>().Add(entity);
            costParameters.Cost = new IntReactiveProperty(cost);
            
            ref var businessTypeParameter = ref _ecsService.GetPool<BusinessTypeParameter>().Add(entity);
            businessTypeParameter.Type = type;

            ref var incomeBuff = ref _ecsService.GetPool<IncomeBuffs>().Add(entity);
            incomeBuff.Buffs = new ReactiveCollection<IncomeBuffData>();
            
            _ecsService.GetPool<BusinessTag>().Add(entity);
            
            return _ecsService.PackEntity(entity);
        }
    }
}