using System;

namespace CodeBase.Logic.Services.Update
{
    public interface IUpdateService
    {
        event Action OnUpdate;
        event Action OnLateUpdate;
        event Action OnFixedUpdate;
    }
}