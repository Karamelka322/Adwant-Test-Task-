namespace CodeBase.Logic.Systems.Businesses
{
    public interface IBusinessBuffSystem
    {
        void AddIncomeBuff(int entity, string id, float multiply);
        bool HasIncomeBuff(int entity, string id);
    }
}