using System;
using CodeBase.Logic.Infrastructure;
using CodeBase.Logic.Infrastructure.Container;
using CodeBase.Logic.Unity.Notifiers;
using UnityEngine;

namespace CodeBase.Logic.Services.Disposer
{
    public class DisposerService
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly ObjectDestroyNotifier _notifier;

        public DisposerService(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
            _notifier = CreateObserver();
            
            _notifier.Destroying += HandleDestroying;
        }
        
        private void HandleDestroying()
        {
            _notifier.Destroying -= HandleDestroying;

            foreach (var disposable in _serviceLocator.GetAll<IDisposable>())
            {
                disposable.Dispose();
            }
        }

        private ObjectDestroyNotifier CreateObserver()
        {
            var gameObject = new GameObject("DestroyObserver");
            return gameObject.AddComponent<ObjectDestroyNotifier>();
        }
    }
}