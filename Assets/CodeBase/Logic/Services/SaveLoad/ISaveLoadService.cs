namespace CodeBase.Logic.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        void Save<TData>(TData data);
        TData Load<TData>();
        bool HasSave<TData>();
    }
}