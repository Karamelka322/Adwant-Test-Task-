using System;
using CodeBase.Data.Static.Enums;

namespace CodeBase.Data.Static.Models
{
    [Serializable]
    public struct BusinessParametersConfig
    {
        public BusinessType Type;
        
        public int StartLevel;
        public int Cost;
        public int Income;
        public int DelayedIncomeTime;
    }
}