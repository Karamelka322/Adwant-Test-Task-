using System.Threading.Tasks;
using CodeBase.Data.Static.Enums;
using UnityEngine;

namespace CodeBase.UI.Elements.Business.Factory
{
    public interface IBusinessFactory
    {
        Task SpawnAsync(BusinessType businessType, Transform parent);
    }
}