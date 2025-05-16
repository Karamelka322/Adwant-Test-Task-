using System;
using System.Text;
using CodeBase.Data.Runtime.ECS.Components.Parameters;
using CodeBase.Logic.Services.ECS;
using CodeBase.Logic.Systems.Businesses.Upgrade;
using Leopotam.EcsLite;
using TMPro;
using UniRx;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Business.Components
{
    public class BusinessLevelUpgradeButton : IDisposable
    {
        private readonly EcsPackedEntity _ecsPackedEntity;
        private readonly IEcsService _ecsService;
        private readonly IBusinessUpgradeSystem _businessUpgradeSystem;
        private readonly TextMeshProUGUI _buttonText;

        private readonly StringBuilder _stringBuilder;
        private readonly IDisposable _disposable;
        
        public BusinessLevelUpgradeButton(
            Button button,
            TextMeshProUGUI buttonText,
            IEcsService ecsService,
            EcsPackedEntity ecsPackedEntity,
            IBusinessUpgradeSystem businessUpgradeSystem)
        {
            _buttonText = buttonText;
            _businessUpgradeSystem = businessUpgradeSystem;
            _ecsService = ecsService;
            _ecsPackedEntity = ecsPackedEntity;
            
            _stringBuilder = new StringBuilder();
            
            int entity = ecsService.UnpackEntity(ecsPackedEntity);
            LevelParameters levelParameters = _ecsService.GetPool<LevelParameters>().Get(entity);
            
            _disposable = levelParameters.UpgradeCost.Subscribe(OnUpgradeCost);

            button.onClick.AddListener(OnClick);
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }
        
        private void OnClick()
        {
            if (_ecsService.TryUnpackEntity(_ecsPackedEntity, out int entity))
            {
                _businessUpgradeSystem.TryUpgradeLevel(entity);
            }
        }
        
        private void OnUpgradeCost(int cost)
        {
            _stringBuilder.Clear();
            
            _stringBuilder.Append("LVL UP \n Цена: ");
            _stringBuilder.Append(cost);
            _stringBuilder.Append("$");
            
            _buttonText.text = _stringBuilder.ToString();
        }
    }
}