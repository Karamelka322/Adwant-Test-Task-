using CodeBase.Data.Runtime.ECS.Components.Parameters;
using CodeBase.Data.Runtime.ECS.Components.Tags;
using CodeBase.Data.Save;
using CodeBase.Data.Static.Enums;
using CodeBase.Logic.Providers.Data.Balance;
using CodeBase.Logic.Providers.Data.Saves;
using CodeBase.Logic.Services.ECS;
using CodeBase.Logic.Systems.Businesses;

namespace CodeBase.Logic.Systems
{
    public class PlayerProgressDataLoader : IPlayerProgressDataLoader
    {
        private readonly IPlayerProgressDataProvider _playerProgressDataProvider;
        private readonly IBalanceDataProvider _balanceDataProvider;
        private readonly IEcsService _ecsService;
        private readonly IBusinessUpgradeSystem _businessUpgradeSystem;
        private readonly IBusinessBuffSystem _businessBuffSystem;

        public PlayerProgressDataLoader(
            IPlayerProgressDataProvider playerProgressDataProvider,
            IEcsService ecsService,
            IBusinessBuffSystem businessBuffSystem,
            IBusinessUpgradeSystem businessUpgradeSystem,
            IBalanceDataProvider balanceDataProvider)
        {
            _businessBuffSystem = businessBuffSystem;
            _businessUpgradeSystem = businessUpgradeSystem;
            _ecsService = ecsService;
            _balanceDataProvider = balanceDataProvider;
            _playerProgressDataProvider = playerProgressDataProvider;
        }

        public void Load()
        {
            LoadBalance();
            LoadBusinesses();
        }

        private void LoadBalance()
        {
            var balance = _playerProgressDataProvider.GetBalance();
            _balanceDataProvider.Set(balance);
        }

        private void LoadBusinesses()
        {
            var businesses = _ecsService.GetFilter<BusinessTag>().Inc<IncomeParameters>()
                .Inc<BusinessTypeParameter>().Inc<LevelParameters>().End();
            
            foreach (var entity in businesses)
            {
                BusinessType type = _ecsService.GetPool<BusinessTypeParameter>().Get(entity).Type;
                
                if (TryGetBusinessData(type, out BusinessSaveData saveData))
                {
                    _businessUpgradeSystem.SetLevel(entity, saveData.Level);
                    _ecsService.GetPool<IncomeParameters>().Get(entity).PayoutProgress.Value = saveData.PayoutProgress;
                    
                    foreach (var buffData in saveData.Buffs)
                    {
                        _businessBuffSystem.AddIncomeBuff(entity, buffData.Id, buffData.Multiplier);
                    }                    
                }
            }
        }
        
        private bool TryGetBusinessData(BusinessType type, out BusinessSaveData saveData)
        {
            var businessData = _playerProgressDataProvider.GetBusinesses();
            
            foreach (var data in businessData)
            {
                if (data.Type == type)
                {
                    saveData = data;
                    return true;
                }
            }
            
            saveData = default;
            return false;
        }
    }
}