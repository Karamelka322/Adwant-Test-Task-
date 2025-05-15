using System;
using System.Collections.Generic;

namespace CodeBase.Data.Save
{
    [Serializable]
    public class PlayerSaveData
    {
        public BalanceSaveData BalanceSaveData;
        public List<BusinessSaveData> BusinessesSaveData;
    }
}