using System;
using System.Text;
using CodeBase.Data.Runtime.ECS.Components.Buffs;
using CodeBase.Data.Static.Models;
using CodeBase.Logic.Infrastructure.Container;
using CodeBase.Logic.Providers.Data.Balance;
using CodeBase.Logic.Services.ECS;
using CodeBase.Logic.Systems.Businesses.Buffs;
using Leopotam.EcsLite;
using TMPro;
using UniRx;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Business.Components
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
             IncomeImprovementConfig config,
             EcsPackedEntity ecsPackedEntity,
             IServiceLocator serviceLocator)
         {
             _balanceDataProvider = serviceLocator.Get<IBalanceDataProvider>();
             _ecsService = serviceLocator.Get<IEcsService>();
             _businessBuffSystem = serviceLocator.Get<IBusinessBuffSystem>();
             
             _id = config.Id;
             _ecsPackedEntity = ecsPackedEntity;
             _multiplier = config.Multiplier;
             _cost = config.Cost;
             _buttonText = buttonText;
             _name = config.Name;

             _stringBuilder = new StringBuilder();

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