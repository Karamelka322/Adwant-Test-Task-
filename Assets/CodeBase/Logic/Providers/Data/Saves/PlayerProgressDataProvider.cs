using System.Collections.Generic;
using CodeBase.Data.Save;
using CodeBase.Logic.Infrastructure.Container;
using CodeBase.Logic.Services.SaveLoad;

namespace CodeBase.Logic.Providers.Data.Saves
{
    public class PlayerProgressDataProvider : IPlayerProgressDataProvider
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly PlayerSaveData _playerSaveData;

        public PlayerProgressDataProvider(IServiceLocator serviceLocator)
        {
            _saveLoadService = serviceLocator.Get<ISaveLoadService>();
            
            if (_saveLoadService.HasSave<PlayerSaveData>() == false)
            {
                var playerSaveData = new PlayerSaveData()
                {
                    BalanceSaveData = new BalanceSaveData(),
                    BusinessesSaveData = new List<BusinessSaveData>()
                };
                
                _saveLoadService.Save(playerSaveData);
                _playerSaveData = playerSaveData;
            }
            else
            {
                _playerSaveData = _saveLoadService.Load<PlayerSaveData>();
            }
        }
        
        public int GetBalance()
        {
            return _playerSaveData.BalanceSaveData.Value;
        }
        
        public void SetBalance(int value)
        {
            _playerSaveData.BalanceSaveData.Value = value;
            _saveLoadService.Save(_playerSaveData);
        }

        public List<BusinessSaveData> GetBusinesses()
        {
            return _playerSaveData.BusinessesSaveData;
        }
        
        public void SetBusinesses(List<BusinessSaveData> businesses)
        { 
            _playerSaveData.BusinessesSaveData.Clear();
            _playerSaveData.BusinessesSaveData.AddRange(businesses);
            
            _saveLoadService.Save(_playerSaveData);
        }
    }
}