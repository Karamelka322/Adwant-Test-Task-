namespace CodeBase.Logic.Systems.Businesses.Buffs
{
    public interface IBusinessBuffSystem
    {
        void AddIncomeBuff(int entity, string id, float multiply);
        bool HasIncomeBuff(int entity, string id);
    }
}