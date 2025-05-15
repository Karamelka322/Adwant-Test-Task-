using System;
using System.Text;
using CodeBase.Data.Runtime.ECS.Components.Parameters;
using CodeBase.Logic.Services.Disposer;
using CodeBase.Logic.Services.ECS;
using Leopotam.EcsLite;
using TMPro;
using UniRx;

namespace CodeBase.UI.Elements.Business
{
    public class BusinessIncomeDisplay : IDisposable
    {
        private readonly IEcsService _ecsService;
        private readonly TextMeshProUGUI _incomeDisplay;
        private readonly IDisposable _disposable;
        private readonly StringBuilder _stringBuilder;

        public BusinessIncomeDisplay(
            TextMeshProUGUI incomeDisplay,
            IEcsService ecsService,
            EcsPackedEntity ecsPackedEntity,
            IDisposerService disposerService)
        {
            _incomeDisplay = incomeDisplay;
            _ecsService = ecsService;

            _stringBuilder = new StringBuilder();
            disposerService.Register(this);

            var entity = _ecsService.UnpackEntity(ecsPackedEntity);
            var income = GetIncome(entity);
            SetText(income);

            _disposable = _ecsService.GetPool<IncomeParameters>().Get(entity).CurrentIncome.Subscribe(OnIncomeChanged);
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }
        
        private void OnIncomeChanged(int income)
        {
            SetText(income);
        }
        
        private int GetIncome(int entity)
        {
            return _ecsService.GetPool<IncomeParameters>().Get(entity).CurrentIncome.Value;
        }
        
        private void SetText(int value)
        {
            _stringBuilder.Clear();
            
            _stringBuilder.Append("Доход:\n");
            _stringBuilder.Append(value);
            _stringBuilder.Append("$");
            
            _incomeDisplay.text = _stringBuilder.ToString();
        }
    }
}