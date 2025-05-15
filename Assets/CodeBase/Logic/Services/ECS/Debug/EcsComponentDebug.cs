using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase.Logic.Services.ECS.Debug
{
    /// <summary>
    /// Служит для представления ECS компонента вне ECS мира
    /// </summary>
    public class EcsComponentDebug : MonoBehaviour
    {
        public int Entity { get; private set; }
        public IEcsPool Pool { get; private set; }
        
        public void Initialize(int entity, IEcsPool pool)
        {
            Entity = entity;
            Pool = pool;
        }
    }
}
