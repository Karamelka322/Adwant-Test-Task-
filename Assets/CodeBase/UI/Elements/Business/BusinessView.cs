using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Business
{
    public class BusinessView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _name;
        
        [SerializeField] 
        private Image _incomeProgressBar;
        
        [SerializeField]
        private TextMeshProUGUI _levelText;
        
        [SerializeField]
        private TextMeshProUGUI _incomeText;
        
        [SerializeField]
        private Button _levelUpgradeButton;
        
        [SerializeField]
        private TextMeshProUGUI _levelUpgradeText;
        
        [SerializeField]
        private Button _firstImprovementButton;
        
        [SerializeField]
        private TextMeshProUGUI _firstImprovementText;
        
        [SerializeField]
        private Button _secondImprovementButton;
        
        [SerializeField]
        private TextMeshProUGUI _secondImprovementText;
        
        public Image IncomeProgressBar => _incomeProgressBar;
        public TextMeshProUGUI LevelDisplay => _levelText;
        public TextMeshProUGUI IncomeDisplay => _incomeText;
        public Button LevelUpgradeButton => _levelUpgradeButton;
        public TextMeshProUGUI Name => _name;
        public TextMeshProUGUI LevelUpgradeText => _levelUpgradeText;

        public Button SecondImprovementButton => _secondImprovementButton;
        public TextMeshProUGUI SecondImprovementText => _secondImprovementText;

        public Button FirstImprovementButton => _firstImprovementButton;
        public TextMeshProUGUI FirstImprovementText => _firstImprovementText;
    }
}