using System.Collections.Generic;
using CodeBase.UI.Elements.Business;
using CodeBase.UI.Elements.Business.Factory;
using CodeBase.UI.Windows.Main.Components;

namespace CodeBase.UI.Windows.Main
{
    public class MainWindowReferences
    {
        public MainWindowView View { get; set; }
        public BalanceDisplay BalanceDisplay { get; set; }
        public List<BusinessReferences> Businesses { get; set; }
    }
}