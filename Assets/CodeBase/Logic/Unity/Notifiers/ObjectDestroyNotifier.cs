using System;
using UnityEngine;

namespace CodeBase.Logic.Unity.Notifiers
{
    public class ObjectDestroyNotifier : MonoBehaviour
    {
        public event Action Destroying;
        
        private void OnDestroy()
        {
            Destroying?.Invoke();
        }
    }
}