using System.Collections.Generic;
using CodeBase.Data.Save;

namespace CodeBase.Logic.Providers.Data.Saves
{
    public interface IPlayerProgressDataProvider
    {
        int GetBalance();
        void SetBalance(int value);
        List<BusinessSaveData> GetBusinesses();
        void SetBusinesses(List<BusinessSaveData> businesses);
    }
}