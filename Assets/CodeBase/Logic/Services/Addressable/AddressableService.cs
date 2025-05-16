using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Logic.Services.Addressable
{
    public class AddressableService : IAddressableService
    {
        public async Task<TObject> LoadAssetAsync<TObject>(string key) where TObject : Object
        {
            AsyncOperationHandle<TObject> loadAssetAsync = UnityEngine.AddressableAssets
                .Addressables.LoadAssetAsync<TObject>(key);
            
            await loadAssetAsync.Task;
            
            return loadAssetAsync.Result;
        }
    }
}