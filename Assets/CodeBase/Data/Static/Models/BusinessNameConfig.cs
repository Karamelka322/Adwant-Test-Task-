using System;
using CodeBase.Data.Static.Enums;

namespace CodeBase.Data.Static.Models
{
    [Serializable]
    public struct BusinessNameConfig
    {
        public BusinessType Type;
        public string Name;
    }
}