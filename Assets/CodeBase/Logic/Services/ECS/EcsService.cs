using System;
using CodeBase.Logic.Services.ECS.Debug;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase.Logic.Services.ECS
{
    public class EcsService : IEcsService
    {
        private EcsWorld _ecsWorld;
        private EcsDebug _ecsDebug;

        public EcsService()
        {
            InitWorld();
            TryInitWorldDebug();
        }
        
        public int AddEntity()
        {
            return _ecsWorld.NewEntity();
        }
        
        public EcsPackedEntity PackEntity(int entity)
        {
            return _ecsWorld.PackEntity(entity);
        }

        public int UnpackEntity(EcsPackedEntity packedEntity)
        {
            bool isUnpacked = packedEntity.Unpack(_ecsWorld, out var entity);

            if (isUnpacked)
            {
                return entity;
            }
            
            throw new ArgumentNullException("Entity not found");
        }
        
        public bool TryUnpackEntity(EcsPackedEntity packedEntity, out int entity)
        {
            return packedEntity.Unpack(_ecsWorld, out entity);
        }
        
        public void DelEntity(int entity)
        {
            _ecsWorld.DelEntity(entity);
        }

        public EcsPool<T> GetPool<T>() where T : struct
        {
            return _ecsWorld.GetPool<T>();
        }
        
        public EcsWorld.Mask GetFilter<T>() where T : struct
        {
            return _ecsWorld.Filter<T>();
        }

        private void InitWorld()
        {
            _ecsWorld = new EcsWorld();
        }

        private void TryInitWorldDebug()
        {
            if (Application.isEditor)
            {
                _ecsDebug = new EcsDebug(_ecsWorld);
            }
        }
    }
}