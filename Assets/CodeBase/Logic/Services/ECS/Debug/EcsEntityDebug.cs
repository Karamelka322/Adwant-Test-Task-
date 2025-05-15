using UnityEngine;

namespace CodeBase.Logic.Services.ECS.Debug
{
    /// <summary>
    /// Служит для представления ECS сущности вне ECS мира
    /// </summary>
    public class EcsEntityDebug : MonoBehaviour
    {
        [SerializeField] private int entity;

        public int Entity => entity;

        public void Initialize(int entity)
        {
            this.entity = entity;
        }
    }
}