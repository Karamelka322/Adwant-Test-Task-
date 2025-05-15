using System;
using CodeBase.Data.Static.Enums;

namespace CodeBase.Data.Static.Models
{
    [Serializable]
    public struct BusinessImprovementConfig
    {
        public BusinessType BusinessType;
        
        public IncomeImprovementConfig FirstImprovement;
        public IncomeImprovementConfig SecondImprovement;
    }
}