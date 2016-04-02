using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MyMoneyManager.ViewModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyMoneyManager.View
{
    /// <summary>
    /// Логика взаимодействия для ExpensesControllerView.xaml
    /// </summary>
    public partial class ExpensesControllerView : UserControl
    {

        private ExpensesControllerViewModel _viewModel;

        public ExpensesControllerViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        public ExpensesControllerView()
        {
            InitializeComponent();
        }
    }
}
