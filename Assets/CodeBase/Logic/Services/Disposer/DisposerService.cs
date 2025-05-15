using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic.Services.Disposer
{
    public class DisposerService : IDisposerService
    {
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        private readonly DestroyObserver _observer;
        
        public DisposerService()
        {
            _observer = CreateObserver();
            
            _observer.OnObjectDestroy += OnDestroy;
        }
        
        public void Register(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }
        
        private void OnDestroy()
        {
            _observer.OnObjectDestroy -= OnDestroy;

            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            
            _disposables.Clear();
        }

        private DestroyObserver CreateObserver()
        {
            var gameObject = new GameObject("DestroyObserver");
            return gameObject.AddComponent<DestroyObserver>();
        }
    }
}