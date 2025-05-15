using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Logic.Services.SaveLoad
{
    /// <summary>
    /// Для сохранения\загрузки данных
    /// </summary>
    [UsedImplicitly]
    public class SaveLoadService : ISaveLoadService
    {
        public void Save<TData>(TData data)
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(nameof(TData), json);
            
            PlayerPrefs.Save();
        }
        
        public bool HasSave<TData>()
        {
            return PlayerPrefs.HasKey(nameof(TData));
        }
        
        public TData Load<TData>()
        {
            string text = PlayerPrefs.GetString(nameof(TData));
            return JsonUtility.FromJson<TData>(text);
        }
    }
}