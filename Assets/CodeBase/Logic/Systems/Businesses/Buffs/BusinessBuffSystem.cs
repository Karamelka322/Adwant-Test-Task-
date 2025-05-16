using CodeBase.Data.Runtime.ECS.Components.Buffs;
using CodeBase.Data.Runtime.ECS.Components.Parameters;
using CodeBase.Data.Static.Models;
using CodeBase.Logic.Formulas.Business;
using CodeBase.Logic.Infrastructure.Container;
using CodeBase.Logic.Services.ECS;
using Leopotam.EcsLite;
using UniRx;

namespace CodeBase.Logic.Systems.Businesses.Buffs
{
    public class BusinessBuffSystem : IBusinessBuffSystem
    {
        private readonly IBusinessFormulas _businessFormulas;
        
        private readonly EcsPool<IncomeBuffs> _incomeBuffBufferPool;
        private readonly EcsPool<IncomeParameters> _incomeParametersPool;

        public BusinessBuffSystem(IServiceLocator serviceLocator)
        {
            _businessFormulas = serviceLocator.Get<IBusinessFormulas>();
            
            IEcsService ecsService = serviceLocator.Get<IEcsService>();
            
            _incomeParametersPool = ecsService.GetPool<IncomeParameters>();
            _incomeBuffBufferPool = ecsService.GetPool<IncomeBuffs>();
        }
        
        public void AddIncomeBuff(int entity, string id, float multiply)
        {
            if (_incomeBuffBufferPool.Has(entity) == false)
            {
                ref IncomeBuffs incomeBuffs = ref _incomeBuffBufferPool.Add(entity);
                incomeBuffs.Buffs = new ReactiveCollection<IncomeBuffData>();
            }
            
            if (HasIncomeBuff(entity, id))
            {
                return;
            }
            
            var buffData = new IncomeBuffData()
            {
                Id = id,
                Multiply = multiply
            };
            
            _incomeBuffBufferPool.Get(entity).Buffs.Add(buffData);
            _incomeParametersPool.Get(entity).CurrentIncome.Value = _businessFormulas.GetIncome(entity);
        }
        
        public bool HasIncomeBuff(int entity, string id)
        {
            if (_incomeBuffBufferPool.Has(entity) == false)
            {
                return false;
            }
            
            foreach (IncomeBuffData buffData in _incomeBuffBufferPool.Get(entity).Buffs)
            {
                if (buffData.Id == id)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}