using System.Threading.Tasks;
using CodeBase.UI.Windows.Main.Factory;

namespace CodeBase.UI.Windows.Main
{
    public class MainWindow : IMainWindow
    {
        private readonly IMainWindowFactory _mainWindowFactory;
        
        public MainWindow(IMainWindowFactory  mainWindowFactory)
        {
            _mainWindowFactory = mainWindowFactory;
        }
        
        public async Task OpenAsync()
        {
            await _mainWindowFactory.SpawnAsync();
        }
    }
}