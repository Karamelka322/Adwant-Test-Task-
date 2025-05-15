using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Main
{
    public class MainWindowView : MonoBehaviour
    {
        [SerializeField] 
        private Transform _businessParent;
        
        [SerializeField] 
        private TextMeshProUGUI _balance;
        
        public Transform BusinessParent => _businessParent;
        public TextMeshProUGUI Balance => _balance;
    }
}