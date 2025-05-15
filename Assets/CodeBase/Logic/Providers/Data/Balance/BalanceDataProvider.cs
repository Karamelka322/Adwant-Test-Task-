using System;
using CodeBase.Logic.Providers.Data.Saves;
using UnityEngine;

namespace CodeBase.Logic.Providers.Data.Balance
{
    public class BalanceDataProvider : IBalanceDataProvider
    {
        private int _balance;
        
        public event Action<int> OnBalanceChanged;
        
        public void Add(int value)
        {
            _balance += value;
            OnBalanceChanged?.Invoke(_balance);
        }
        
        public void Take(int value)
        {
            _balance = Mathf.Clamp(_balance - value, 0, int.MaxValue);
            OnBalanceChanged?.Invoke(_balance);
        }

        public bool Has(int value)
        {
            return _balance >= value;
        }

        public void Set(int value)
        {
            _balance = value;
            OnBalanceChanged?.Invoke(_balance);
        }

        public int Get()
        {
            return _balance;
        }
    }
}