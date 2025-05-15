using Leopotam.EcsLite;

namespace CodeBase.Logic.Services.ECS
{
    public interface IEcsService
    {
        int AddEntity();
        void DelEntity(int entity);
        
        EcsPool<T> GetPool<T>() where T : struct;
        EcsWorld.Mask GetFilter<T>() where T : struct;
        
        EcsPackedEntity PackEntity(int entity);
        int UnpackEntity(EcsPackedEntity packedEntity);
        bool TryUnpackEntity(EcsPackedEntity packedEntity, out int entity);
    }
}