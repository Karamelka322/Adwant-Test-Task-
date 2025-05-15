using System;
using System.Collections.Generic;
using CodeBase.Data.Static.Enums;

namespace CodeBase.Data.Save
{
    [Serializable]
    public struct BusinessSaveData
    {
        public BusinessType Type;
        
        public int Level;
        public float PayoutProgress;
        
        public List<IncomeBuffSaveData> Buffs;
    }
}