using System;
using UnityEngine;

namespace CodeBase.Logic.Unity.Notifiers
{
    public class ObjectUpdateNotifier : MonoBehaviour
    {
        public event Action Updating;
        
        private void Update()
        {
            Updating?.Invoke();
        }
    }
}