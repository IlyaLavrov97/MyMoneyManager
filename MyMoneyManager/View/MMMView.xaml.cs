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
using System.Windows.Shapes;
using MyMoneyManager.ViewModel;

namespace MyMoneyManager.View
{

    
    /// <summary>
    /// Логика взаимодействия для MMMView.xaml
    /// </summary>
    public partial class MMMView : Window
    {

        private MMMViewModel _viewModel;

        public MMMViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        public MMMView()
        {
            InitializeComponent();
            tabControl.Items.Add(new TabItem
            {
                Header = "Расходы",
                Content = new ExpensesControllerView
                {
                    DataContext = new ExpensesControllerViewModel()
                }
            });
        }
    }
}
