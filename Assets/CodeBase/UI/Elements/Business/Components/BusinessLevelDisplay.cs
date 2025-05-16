using System;
using System.Text;
using CodeBase.Data.Runtime.ECS.Components.Parameters;
using CodeBase.Logic.Services.ECS;
using Leopotam.EcsLite;
using TMPro;
using UniRx;

namespace CodeBase.UI.Elements.Business.Components
{
    public class BusinessLevelDisplay : IDisposable
    {
        private readonly IEcsService _ecsService;
        private readonly TextMeshProUGUI _levelDisplay;
        private readonly IDisposable _disposable;
        private readonly StringBuilder _stringBuilder;
        
        public BusinessLevelDisplay(
            TextMeshProUGUI levelDisplay,
            IEcsService ecsService,
            EcsPackedEntity ecsPackedEntity)
        {
            _levelDisplay = levelDisplay;
            _ecsService = ecsService;

            _stringBuilder = new StringBuilder();

            int entity = _ecsService.UnpackEntity(ecsPackedEntity);
            int leve = GetLevel(entity);
            
            SetText(leve);
            
            _disposable = _ecsService.GetPool<LevelParameters>().Get(entity).Level.Subscribe(OnLevelChanged);
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }
        
        private void OnLevelChanged(int level)
        {
            SetText(level);
        }
        
        private int GetLevel(int entity)
        {
            return _ecsService.GetPool<LevelParameters>().Get(entity).Level.Value;
        }
        
        private void SetText(int value)
        {
            _stringBuilder.Clear();
            
            _stringBuilder.Append("LVL:\n");
            _stringBuilder.Append(value);
            
            _levelDisplay.text = _stringBuilder.ToString();
        }
    }
}