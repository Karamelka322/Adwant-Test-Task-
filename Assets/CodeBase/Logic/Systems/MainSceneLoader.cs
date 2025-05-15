using System.Threading.Tasks;
using CodeBase.UI.Windows.Main;
using UnityEngine;

namespace CodeBase.Logic.Systems
{
    public class MainSceneLoader
    {
        private readonly IMainWindow _mainWindow;
        private readonly IPlayerProgressDataLoader _playerProgressDataLoader;

        public MainSceneLoader(IMainWindow mainWindow, IPlayerProgressDataLoader playerProgressDataLoader)
        {
            _mainWindow = mainWindow;
            _playerProgressDataLoader = playerProgressDataLoader;
            
            LoadAsync().ContinueWith(t =>
            {
                if (t.Exception != null)
                {
                    Debug.LogError(t.Exception);
                }
                
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private async Task LoadAsync()
        {
            await _mainWindow.OpenAsync();
            _playerProgressDataLoader.Load();
        }
    }
}