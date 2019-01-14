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
using WB.SignalR.DataProvider.DataProvider;
using WB.SignalR.ViewModel;

namespace WrestlingBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            Execute.InitializeWithDispatcher();

            _vm = new MainViewModel(new SignalRHubSync(), new WpfContext());
            this.DataContext = _vm;
            _vm.Load();
        }
    }
}
