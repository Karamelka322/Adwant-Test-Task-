using System.Collections.Generic;
using CodeBase.Data.Static.Models;
using UnityEngine;

namespace CodeBase.Data.Static.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Game/BusinessesParametersSettings", fileName = nameof(BusinessesParametersSettings))]
    public class BusinessesParametersSettings : ScriptableObject
    {
        [SerializeField]
        private List<BusinessParametersConfig> _business;
        
        public List<BusinessParametersConfig> Business => _business;
    }
}