using System;
using System.Collections.Generic;
using CodeBase.Data.Runtime.ECS.Components.Buffs;
using CodeBase.Data.Runtime.ECS.Components.Parameters;
using CodeBase.Data.Runtime.ECS.Components.Tags;
using CodeBase.Data.Save;
using CodeBase.Logic.Infrastructure.Container;
using CodeBase.Logic.Providers.Data.Balance;
using CodeBase.Logic.Providers.Data.Saves;
using CodeBase.Logic.Services.ECS;
using UnityEngine;

namespace CodeBase.Logic.Systems.SaveLoad
{
    public class PlayerProgressDataSaver : IDisposable
    {
        private readonly IPlayerProgressDataProvider _playerSaveDataProvider;
        private readonly IBalanceDataProvider _balanceDataProvider;
        private readonly IEcsService _ecsService;

        public PlayerProgressDataSaver(IServiceLocator serviceLocator)
        {
            _playerSaveDataProvider = serviceLocator.Get<IPlayerProgressDataProvider>();
            _ecsService = serviceLocator.Get<IEcsService>();
            _balanceDataProvider = serviceLocator.Get<IBalanceDataProvider>();
            
            Application.focusChanged += OnApplicationFocusChanged;
            Application.quitting += OnApplicationQuitting;
        }
        
        public void Dispose()
        {
            Application.focusChanged -= OnApplicationFocusChanged;
            Application.quitting -= OnApplicationQuitting;
        }
        
        private void OnApplicationFocusChanged(bool hasFocus)
        {
            if (hasFocus == false)
            {
                Save();
            }
        }
        
        private void OnApplicationQuitting()
        {
            Save();
        }
        
        private void Save()
        {
            _playerSaveDataProvider.SetBalance(GetBalanceSaveData());
            _playerSaveDataProvider.SetBusinesses(GetBusinessesSaveData());
        }

        private int GetBalanceSaveData()
        {
            return _balanceDataProvider.Get();
        }

        private List<BusinessSaveData> GetBusinessesSaveData()
        {
            List<BusinessSaveData> businesses = new List<BusinessSaveData>();

            var businessesFilter = _ecsService.GetFilter<BusinessTag>()
                .Inc<IncomeParameters>().Inc<LevelParameters>().End();
            
            foreach (int entity in businessesFilter)
            {
                BusinessSaveData saveData = new BusinessSaveData()
                {
                    Type = _ecsService.GetPool<BusinessTypeParameter>().Get(entity).Type,
                    Level = _ecsService.GetPool<LevelParameters>().Get(entity).Level.Value,
                    PayoutProgress = _ecsService.GetPool<IncomeParameters>().Get(entity).PayoutProgress.Value,
                    Buffs = GetIncomeBuffsSaveData(entity),
                };
                
                businesses.Add(saveData);
            }
            
            return businesses;
        }

        private List<IncomeBuffSaveData> GetIncomeBuffsSaveData(int entity)
        {
            List<IncomeBuffSaveData> incomeBuffs = new List<IncomeBuffSaveData>();
            
            if (_ecsService.GetPool<IncomeBuffs>().Has(entity) == false)
            {
                return incomeBuffs;
            }
            
            foreach (var buffData in _ecsService.GetPool<IncomeBuffs>().Get(entity).Buffs)
            {
                IncomeBuffSaveData saveData = new IncomeBuffSaveData()
                {
                    Id = buffData.Id,
                };
                
                incomeBuffs.Add(saveData);
            }
            
            return incomeBuffs;
        }
    }
}