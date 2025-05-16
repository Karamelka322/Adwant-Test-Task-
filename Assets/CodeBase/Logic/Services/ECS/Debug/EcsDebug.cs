using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Services.ECS.Debug
{
    /// <summary>
    /// Служит для отладки текущих ECS сущностей/компонентов на сцене в виде игровых обьектов
    /// </summary>
    public class EcsDebug : IEcsWorldEventListener
    {
        private readonly EcsWorld _world;
        
        private readonly GameObject _worldObject;
        private readonly GameObject _entitiesParent;

        private readonly Dictionary<int, GameObject> _entities = new();

        public EcsDebug(EcsWorld world)
        {
            _world = world;
            
            _world.AddEventListener(this);
            
            _worldObject = CreateWorldObject();
            _entitiesParent = CreateDebugObject("Entities", _worldObject.transform);
        }

        /// Обработка события добавления новой сущности
        public void OnEntityCreated(int entity)
        {
            GameObject entityObject = CreateDebugObject(entity.ToString(), _entitiesParent.transform);
            
            EcsEntityDebug debug = entityObject.gameObject.AddComponent<EcsEntityDebug>();
            debug.Initialize(entity);
            
            _entities.Add(entity, entityObject);
        }
        
        /// Обработка события изменения сущности в ecs пулах
        public void OnEntityChanged(int entity, short poolId, bool added)
        {
            if (_entities.ContainsKey(entity) == false || _entities[entity] == null)
            {
                return;
            }

            IEcsPool pool = _world.GetPoolById(poolId);
            
            if (added)
            {
                var debug = _entities[entity].gameObject.AddComponent<EcsComponentDebug>();
                debug.Initialize(entity, pool);
            }
            else
            {
                EcsComponentDebug[] components = _entities[entity].GetComponents<EcsComponentDebug>();

                for (int i = 0; i < components.Length; i++)
                {
                    if (components[i].Entity == entity && components[i].Pool == pool)
                    {
                        Object.Destroy(components[i]);
                        break;
                    } 
                }
            }
        }

        /// Обработка события удаления сущности
        public void OnEntityDestroyed(int entity)
        {
            if (_entities.Remove(entity, out GameObject gameObject))
            {
                Object.Destroy(gameObject);
            }
        }

        public void OnFilterCreated(EcsFilter filter) { }
        public void OnWorldResized(int newSize) { }

        /// Обработка события удаления ecs мира
        public void OnWorldDestroyed(EcsWorld world)
        {
            Object.Destroy(_worldObject);
        }

        private GameObject CreateWorldObject()
        {
            GameObject world = CreateDebugObject("ECS");
            Object.DontDestroyOnLoad(world);

            return world;
        }
        
        /// Создать отладочный обьект
        private static GameObject CreateDebugObject(string name, Transform parent = null)
        {
            var obj = new GameObject(name)
            {
                hideFlags = HideFlags.NotEditable,
            
                transform =
                {
                    parent = parent
                }
            };
            
            return obj;
        }
    }
}