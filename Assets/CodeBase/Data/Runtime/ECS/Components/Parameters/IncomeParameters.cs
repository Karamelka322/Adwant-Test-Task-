using UniRx;

namespace CodeBase.Data.Runtime.ECS.Components.Parameters
{
    public struct IncomeParameters
    {
        public IntReactiveProperty BaseIncome;
        public IntReactiveProperty CurrentIncome;
        
        public IntReactiveProperty PayoutDelay;
        public FloatReactiveProperty PayoutProgress;
    }
}