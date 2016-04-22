using System.Windows;
using System.Windows.Controls;

namespace MyMoneyManager.View
{
    /// <summary>
    /// Логика взаимодействия для ExpensesControllerView.xaml
    /// </summary>
    public partial class ExpensesControllerView : UserControl
    {

        public ExpensesControllerView()
        {
            InitializeComponent();
            placeHolder.GotFocus += PlaceHolder_GotFocus;
            placeHolder.LostFocus += PlaceHolder_LostFocus;
        }

        private void PlaceHolder_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(placeHolder.Text))
                placeHolder.Text = "Поиск...";
        }

        private void PlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            placeHolder.Text = "";
        }
    }
}
