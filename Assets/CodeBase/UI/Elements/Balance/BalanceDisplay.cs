using System;
using System.Text;
using CodeBase.Logic.Providers.Data.Balance;
using CodeBase.Logic.Services.Disposer;
using TMPro;

namespace CodeBase.UI.Elements.Balance
{
    public class BalanceDisplay : IDisposable
    {
        private readonly IBalanceDataProvider _balanceDataProvider;
        private readonly TextMeshProUGUI _label;
        private readonly StringBuilder _stringBuilder;
        
        public BalanceDisplay(
            TextMeshProUGUI label, 
            IBalanceDataProvider balanceDataProvider, 
            IDisposerService disposerService)
        {
            _balanceDataProvider = balanceDataProvider;
            _label = label;
            _stringBuilder = new StringBuilder();
            
            disposerService.Register(this);
            
            SetBalance(_balanceDataProvider.Get());
            
            _balanceDataProvider.OnBalanceChanged += OnBalanceChanged;
        }

        public void Dispose()
        {
            _balanceDataProvider.OnBalanceChanged -= OnBalanceChanged;
        }

        private void OnBalanceChanged(int balance)
        {
            SetBalance(balance);
        }

        private void SetBalance(int balance)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append("Баланс: ");
            _stringBuilder.Append(balance);
            _stringBuilder.Append("$");
            
            _label.text = _stringBuilder.ToString();
        }
    }
}