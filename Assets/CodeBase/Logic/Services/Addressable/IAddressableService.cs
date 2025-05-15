using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Services.Addressable
{
    public interface IAddressableService
    {
        Task<TObject> LoadAssetAsync<TObject>(string key) where TObject : Object;
    }
}