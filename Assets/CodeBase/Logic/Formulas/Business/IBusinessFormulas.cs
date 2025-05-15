namespace CodeBase.Logic.Formulas.Business
{
    public interface IBusinessFormulas
    {
        int GetIncome(int entity);
        int GetUpgradeLevelCost(int currentLevel, int businessCost);
    }
}