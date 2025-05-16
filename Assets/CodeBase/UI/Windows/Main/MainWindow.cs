using System;
using System.Threading.Tasks;
using CodeBase.Logic.Infrastructure;
using CodeBase.Logic.Infrastructure.Container;
using CodeBase.UI.Elements.Business;
using CodeBase.UI.Elements.Business.Factory;
using CodeBase.UI.Windows.Main.Factory;

namespace CodeBase.UI.Windows.Main
{
    public class MainWindow : IMainWindow, IDisposable
    {
        private readonly IMainWindowFactory _mainWindowFactory;
        
        private MainWindowReferences _references;

        public MainWindow(IServiceLocator serviceLocator)
        {
            _mainWindowFactory = serviceLocator.Get<IMainWindowFactory>();
        }
        
        public async Task OpenAsync()
        {
            _references = await _mainWindowFactory.SpawnAsync();
        }
        
        public void Dispose()
        {
            if (_references == null)
            {
                return;
            }
            
            _references.BalanceDisplay.Dispose();
            
            foreach (BusinessReferences businessReferences in _references.Businesses)
            {
                businessReferences.IncomeDisplay.Dispose();
                businessReferences.LevelDisplay.Dispose();
                businessReferences.PayoutDisplay.Dispose();
                businessReferences.FirstImprovementButton.Dispose();
                businessReferences.SecondImprovementButton.Dispose();
                businessReferences.LevelUpgradeButton.Dispose();
            }
        }
    }
}