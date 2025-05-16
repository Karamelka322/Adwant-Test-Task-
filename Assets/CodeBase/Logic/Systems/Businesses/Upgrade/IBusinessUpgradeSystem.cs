namespace CodeBase.Logic.Systems.Businesses.Upgrade
{
    public interface IBusinessUpgradeSystem
    {
        bool TryUpgradeLevel(int entity);
        void SetLevel(int entity, int level);
    }
}