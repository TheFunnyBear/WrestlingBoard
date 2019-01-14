using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WB.SignalR.ViewModel;

namespace WrestlingBoard
{
    /// <summary>
    /// Interaction logic for WrestlerControl.xaml
    /// </summary>
    public partial class WrestlerControl : UserControl
    {
        private WrestlerControlViewModel _vm;

        public WrestlerControl()
        {
            InitializeComponent();
            Execute.InitializeWithDispatcher();

            _vm = new WrestlerControlViewModel();
            this.DataContext = _vm;
            _vm.Load();
        }
    }
}
