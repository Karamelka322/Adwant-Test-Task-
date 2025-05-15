using System;
using System.Text;
using CodeBase.Data.Runtime.ECS.Components.Parameters;
using CodeBase.Logic.Providers.Data.Balance;
using CodeBase.Logic.Services.Disposer;
using CodeBase.Logic.Services.ECS;
using CodeBase.Logic.Systems.Businesses;
using Leopotam.EcsLite;
using TMPro;
using UniRx;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Business
{
    public class BusinessIncomeBuffButton : IDisposable
    {
         private readonly IBusinessBuffSystem _businessBuffSystem;
         private readonly IBalanceDataProvider _balanceDataProvider;
         private readonly IEcsService _ecsService;
         private readonly IDisposable _disposable;

         private readonly EcsPackedEntity _ecsPackedEntity;
         private readonly StringBuilder _stringBuilder;
         private readonly TextMeshProUGUI _buttonText;

         private readonly string _name;
         private readonly int _cost;
         private readonly float _multiplier;
         private readonly string _id;

         public BusinessIncomeBuffButton(
             Button button,
             TextMeshProUGUI buttonText,
             string name,
             string id,
             int cost,
             float multiplier,
             IEcsService ecsService,
             EcsPackedEntity ecsPackedEntity,
             IBusinessBuffSystem businessBuffSystem,
             IBalanceDataProvider balanceDataProvider,
             IDisposerService disposerService)
         {
             _balanceDataProvider = balanceDataProvider;
             _id = id;
             _ecsPackedEntity = ecsPackedEntity;
             _ecsService = ecsService;
             _businessBuffSystem = businessBuffSystem;
             _multiplier = multiplier;
             _cost = cost;
             _buttonText = buttonText;
             _name = name;

             _stringBuilder = new StringBuilder();
             disposerService.Register(this);

             int entity = _ecsService.UnpackEntity(ecsPackedEntity);
             _disposable = _ecsService.GetPool<IncomeBuffs>().Get(entity).Buffs.ObserveAdd().Subscribe(OnAddBuff);

             UpdateText();

             button.onClick.AddListener(OnClick);
         }
         
         public void Dispose()
         {
             _disposable?.Dispose();
         }

         private void OnClick()
         {
             int entity = _ecsService.UnpackEntity(_ecsPackedEntity);

             if (_businessBuffSystem.HasIncomeBuff(entity, _id) == false)
             {
                 if (_balanceDataProvider.Has(_cost))
                 {
                     _balanceDataProvider.Take(_cost);
                     _businessBuffSystem.AddIncomeBuff(entity, _id, _multiplier);

                     UpdateText();
                 }
             }
         }

         private void OnAddBuff(CollectionAddEvent<IncomeBuffData> buffData)
         {
             if (buffData.Value.Id == _id)
             {
                 UpdateText();
             }
         }

         private void UpdateText()
         {
             _stringBuilder.Clear();

             _stringBuilder.Append(_name).AppendLine();
             _stringBuilder.Append("Доход: +").Append(100f * _multiplier).Append("%").AppendLine();

             int entity = _ecsService.UnpackEntity(_ecsPackedEntity);

             if (_businessBuffSystem.HasIncomeBuff(entity, _id))
             {
                 _stringBuilder.Append("Куплено");
             }
             else
             {
                 _stringBuilder.Append("Цена: ").Append(_cost).Append("$");
             }

             _buttonText.text = _stringBuilder.ToString();
         }
    }
}