using CodeBase.Data.Runtime.ECS.Components.Parameters;
using CodeBase.Logic.Services.ECS;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase.Logic.Formulas.Business
{
    public class BusinessFormulas : IBusinessFormulas
    {
        private readonly EcsPool<LevelParameters> _levelParametersPool;
        private readonly EcsPool<IncomeParameters> _incomeParametersPool;
        private readonly EcsPool<IncomeBuffs> _incomeBuffsPool;

        public BusinessFormulas(IEcsService ecsService)
        {
            _levelParametersPool = ecsService.GetPool<LevelParameters>();
            _incomeParametersPool = ecsService.GetPool<IncomeParameters>();
            _incomeBuffsPool = ecsService.GetPool<IncomeBuffs>();
        }
        
        public int GetIncome(int entity)
        {
            var level = Mathf.Clamp(_levelParametersPool.Get(entity).Level.Value, 1, int.MaxValue);
            int income = level * _incomeParametersPool.Get(entity).BaseIncome.Value;

            if (_incomeBuffsPool.Has(entity))
            {
                var incomeBuffs = _incomeBuffsPool.Get(entity);

                foreach (var buff in incomeBuffs.Buffs)
                {
                    income = (int)(income * (1 + buff.Multiply));
                }
            }
            
            return income;
        }
        
        public int GetUpgradeLevelCost(int currentLevel, int businessCost)
        {
            return (currentLevel + 1) * businessCost;
        }
    }
}