using CodeBase.UI.Elements.Business.Components;

namespace CodeBase.UI.Elements.Business
{
    public class BusinessReferences
    {
        public BusinessView View { get; set; }
        public BusinessIncomeBuffButton FirstImprovementButton { get; set; }
        public BusinessIncomeBuffButton SecondImprovementButton { get; set; }
        public BusinessLevelUpgradeButton LevelUpgradeButton { get; set; }
        public BusinessPayoutDisplay PayoutDisplay { get; set; }
        public BusinessIncomeDisplay IncomeDisplay { get; set; }
        public BusinessLevelDisplay LevelDisplay { get; set; }
    }
}