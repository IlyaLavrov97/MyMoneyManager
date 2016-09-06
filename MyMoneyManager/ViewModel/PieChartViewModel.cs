using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using MyMoneyManager.ViewModel.ClassesForVM;
using System.Windows.Input;
using MyMoneyManager.ViewModel.ClassesForVM.Mediator;
using MyMoneyManager.Model.Expenses.ViewObject;
using MyMoneyManager.Model;

namespace MyMoneyManager.ViewModel
{
    public class PieChartViewModel: ViewModelBase,
        IConnectedViewModel
    {
        private SeriesCollection series = new SeriesCollection();

        public SeriesCollection Series
        {
            get { return series; }
            set
            {
                series = value;
                OnPropertyChanged("Series");
            }
        }

        private Dictionary<string, double> expensesForSeries = new Dictionary<string, double>();

        MediatorForVM VVM;

        public PieChartViewModel()
        {
            Init();
        }

        private void Init()
        {
            VVM = MediatorForVM.Instance;
            VVM.PieChart = this;
        }

        public void Send(IConnectedViewModel to, IViewElement message)
        {
            throw new NotImplementedException();
        }

        public void NotifyAbout(IViewElement message)
        {
            //double myValue;
            //if (message != null)
            //{
            //    Series.Clear();

            //    if(expensesForSeries.TryGetValue(message.ExpensesType.ToString(), out myValue))
            //    {
            //        myValue = myValue + message.Expenditure;
            //        expensesForSeries[message.ExpensesType.ToString()] = myValue;
            //    }
            //    else
            //    {
            //        expensesForSeries.Add(message.ExpensesType.ToString(), message.Expenditure);
            //    }
            //}
            //else
            //{
            //    foreach (var expForSer in expensesForSeries.OrderByDescending(item => item.Value))
            //    {
            //        Series.Add(new PieSeries {
            //            Values = new ChartValues<double> { expForSer.Value },
            //            Title = expForSer.Key,
            //            DataLabels = true
            //        });
            //    }

            //    expensesForSeries.Clear();
            //}
        }


    }
}
