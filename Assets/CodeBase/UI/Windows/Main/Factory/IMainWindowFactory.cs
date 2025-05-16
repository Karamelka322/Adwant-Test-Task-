using System.Threading.Tasks;

namespace CodeBase.UI.Windows.Main.Factory
{
    public interface IMainWindowFactory
    {
        Task<MainWindowReferences> SpawnAsync();
    }
}