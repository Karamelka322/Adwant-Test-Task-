using System;
using CodeBase.Logic.Services.Disposer;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Services.Update
{
    public class UpdateService : IUpdateService, IDisposable
    {
        private readonly UpdateObserver _observer;
        
        public event Action OnUpdate;
        public event Action OnLateUpdate;
        public event Action OnFixedUpdate;
        
        public UpdateService(IDisposerService disposerService)
        {
            _observer = CreateObserver();
            
            _observer.OnUpdate += InvokeUpdate;
            _observer.OnLateUpdate += InvokeLateUpdate;
            _observer.OnFixedUpdate += InvokeFixedUpdate;
            
            disposerService.Register(this);
        }
        
        public void Dispose()
        {
            _observer.OnUpdate -= InvokeUpdate;
            _observer.OnLateUpdate -= InvokeLateUpdate;
            _observer.OnFixedUpdate -= InvokeFixedUpdate;
        }

        private void InvokeFixedUpdate() => OnFixedUpdate?.Invoke();
        private void InvokeLateUpdate() => OnLateUpdate?.Invoke();
        private void InvokeUpdate() => OnUpdate?.Invoke();

        private UpdateObserver CreateObserver()
        {
            var observer = new GameObject("GameUpdate")
                .AddComponent<UpdateObserver>();
            
            Object.DontDestroyOnLoad(observer);
            
            return observer;
        }
    }
}