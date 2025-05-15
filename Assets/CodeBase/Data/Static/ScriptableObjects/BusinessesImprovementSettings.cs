using System.Collections.Generic;
using CodeBase.Data.Static.Models;
using UnityEngine;

namespace CodeBase.Data.Static.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Game/BusinessesImprovementsSettings", fileName = nameof(BusinessesImprovementSettings))]
    public class BusinessesImprovementSettings : ScriptableObject
    {
        [SerializeField]
        private List<BusinessImprovementConfig> _business;
        
        public List<BusinessImprovementConfig> Business => _business;
    }
}