using CodeBase.Data.Static.Models;
using UniRx;

namespace CodeBase.Data.Runtime.ECS.Components.Buffs
{
    public struct IncomeBuffs
    {
        public ReactiveCollection<IncomeBuffData> Buffs;
    }
}