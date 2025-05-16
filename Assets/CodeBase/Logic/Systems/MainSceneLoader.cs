using System.Threading.Tasks;
using CodeBase.Logic.Infrastructure;
using CodeBase.Logic.Infrastructure.Container;
using CodeBase.Logic.Systems.SaveLoad;
using CodeBase.UI.Windows.Main;
using UnityEngine;

namespace CodeBase.Logic.Systems
{
    public class MainSceneLoader
    {
        private readonly IMainWindow _mainWindow;
        private readonly IPlayerProgressDataLoader _playerProgressDataLoader;
        
        public MainSceneLoader(IServiceLocator serviceLocator)
        {
            _mainWindow = serviceLocator.Get<IMainWindow>();
            _playerProgressDataLoader = serviceLocator.Get<IPlayerProgressDataLoader>();
            
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