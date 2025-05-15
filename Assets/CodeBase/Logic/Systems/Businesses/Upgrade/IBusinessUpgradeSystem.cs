namespace CodeBase.Logic.Systems.Businesses
{
    public interface IBusinessUpgradeSystem
    {
        bool TryUpgradeLevel(int entity);
        void SetLevel(int entity, int level);
    }
}