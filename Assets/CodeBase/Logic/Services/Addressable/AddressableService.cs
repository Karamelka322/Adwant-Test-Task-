using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Services.Addressable
{
    public class AddressableService : IAddressableService
    {
        public async Task<TObject> LoadAssetAsync<TObject>(string key) where TObject : Object
        {
            var loadAssetAsync = UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<TObject>(key);
            
            await loadAssetAsync.Task;
            
            return loadAssetAsync.Result;
        }
    }
}