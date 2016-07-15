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

namespace MyMoneyManager.View
{
    /// <summary>
    /// Логика взаимодействия для LineChartView.xaml
    /// </summary>
    public partial class LineChartView : UserControl
    {
        public LineChartView()
        {
            InitializeComponent();
        }



        private void LineChartView_OnLoaded()
        {
            //this is just to see animation everytime you click next
            Chart.Update();
        }
    }
}
