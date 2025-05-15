using System;

namespace CodeBase.Logic.Services.Disposer
{
    public interface IDisposerService
    {
        void Register(IDisposable disposable);
    }
}