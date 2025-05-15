using System;

namespace CodeBase.Logic.Providers.Data.Balance
{
    public interface IBalanceDataProvider
    {
        void Add(int value);
        void Take(int value);
        bool Has(int value);
        int Get();
        event Action<int> OnBalanceChanged;
        void Set(int value);
    }
}