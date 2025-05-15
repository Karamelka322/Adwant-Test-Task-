#if UNITY_EDITOR

using Sisus.ComponentNames;
using UnityEditor;

namespace CodeBase.Logic.Services.ECS.Debug
{
    /// <summary>
    /// Для отрисовки данных ECS сущности в компоненьте EcsEntityDebug
    /// </summary>
    [CustomEditor(typeof(EcsEntityDebug))]
    public class EcsEntityDebugEditor : Editor
    {
        private EcsEntityDebug _debug;
        
        private const string ComponentName = "Entity";

        private void Awake()
        {
            _debug = target as EcsEntityDebug;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            _debug.SetName(ComponentName, false);
        }
    }
}

#endif