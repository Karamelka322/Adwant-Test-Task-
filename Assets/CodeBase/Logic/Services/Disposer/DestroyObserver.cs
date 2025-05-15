using System;
using UnityEngine;

namespace CodeBase.Logic.Services.Disposer
{
    public class DestroyObserver : MonoBehaviour
    {
        public event Action OnObjectDestroy;
        
        private void OnDestroy()
        {
            OnObjectDestroy?.Invoke();
        }
    }
}