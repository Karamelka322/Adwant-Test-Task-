using System.Collections.Generic;
using CodeBase.Data.Static.Models;
using UnityEngine;

namespace CodeBase.Data.Static.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Game/BusinessesNamesSettings", fileName = nameof(BusinessesNameSettings))]
    public class BusinessesNameSettings : ScriptableObject
    {
        [SerializeField]
        private List<BusinessNameConfig> _business;
        
        public List<BusinessNameConfig> Business => _business;
    }
}