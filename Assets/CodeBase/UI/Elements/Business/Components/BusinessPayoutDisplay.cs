using System;
using CodeBase.Data.Runtime.ECS.Components.Parameters;
using CodeBase.Logic.Services.ECS;
using Leopotam.EcsLite;
using TMPro;
using UniRx;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Business.Components
{
    public class BusinessPayoutDisplay : IDisposable
    {
        private readonly TextMeshProUGUI _incomeDisplay;
        private readonly IDisposable _disposable;
        private readonly Image _progressBar;

        public BusinessPayoutDisplay(
            Image progressBar,
            IEcsService ecsService,
            EcsPackedEntity ecsPackedEntity)
        {
            _progressBar = progressBar;
            
            int entity = ecsService.UnpackEntity(ecsPackedEntity);
            IncomeParameters incomeParameters = ecsService.GetPool<IncomeParameters>().Get(entity);
            
            SetPayoutProgress(incomeParameters.PayoutProgress.Value);
            
            _disposable = incomeParameters.PayoutProgress.Subscribe(OnPayoutProgressChanged);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void OnPayoutProgressChanged(float progress)
        {
            SetPayoutProgress(progress);
        }

        private void SetPayoutProgress(float progress)
        {
            _progressBar.fillAmount = progress;
        }
    }
}