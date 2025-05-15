using CodeBase.Data.Static.Enums;
using Leopotam.EcsLite;

namespace CodeBase.UI.Elements.Business.Provider
{
    public interface IBusinessEntityProvider
    {
        EcsPackedEntity Provide(int level, int income, int cost, int payoutDelay, BusinessType type);
    }
}