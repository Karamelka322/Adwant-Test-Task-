using System;
using CodeBase.Logic.Services.Disposer;
using CodeBase.Logic.Unity.Notifiers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Services.Update
{
    public class UpdateService : IUpdateService, IDisposable
    {
        private readonly ObjectUpdateNotifier _notifier;
        
        public event Action OnUpdate;
        public event Action OnLateUpdate;
        public event Action OnFixedUpdate;
        
        public UpdateService()
        {
            _notifier = CreateObserver();
            
            _notifier.Updating += HandleUpdating;
        }
        
        public void Dispose()
        {
            _notifier.Updating -= HandleUpdating;
        }
        
        private void HandleUpdating()
        {
            OnUpdate?.Invoke();
        }

        private ObjectUpdateNotifier CreateObserver()
        {
            var observer = new GameObject("GameUpdate").AddComponent<ObjectUpdateNotifier>();
            
            Object.DontDestroyOnLoad(observer.gameObject);
            
            return observer;
        }
    }
}