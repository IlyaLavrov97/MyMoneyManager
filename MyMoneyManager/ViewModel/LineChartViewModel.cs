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

namespace MyMoneyManager.ViewModel
{
    public class LineChartViewModel : ViewModelBase,
        IConnectedExpensesViewModel
        
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

        private List<ViewExpensesInfo> newExpensesInfos = new List<ViewExpensesInfo>();

        public List<ViewExpensesInfo> NewExpensesInfos
        {
            get { return newExpensesInfos; }
            set
            {
                newExpensesInfos = value;
            }
        }

        private List<string> axisX = new List<string>();

        public List<string> AxisX
        {
            get { return axisX; }
            set
            {
                axisX = value;
                OnPropertyChanged("AxisX");
            }
        }

        private LineSeries ExpensesSeries = new LineSeries
        {
            Title = "Расходы",
            Values = new ChartValues<double>()
        };

        MediatorForVM VVM;

        public LineChartViewModel()
        {
            Init();
            RemovePointsOnClickCommand = new RelayCommand(arg => RemovePointsOnClick(), x => True());
            AddPointsOnClickCommand = new RelayCommand(arg => AddPointsOnClick(), x => True());
            RemoveSeriesOnClickCommand = new RelayCommand(arg => RemoveSeriesOnClick(), x => True());
            AddSeriesOnClickCommand = new RelayCommand(arg => AddSeriesOnClick(), x => True());
            TestOnClickCommand = new RelayCommand(arg => TestOnClick(), x => True());
        }

        private void Init()
        {
            Series.Add(ExpensesSeries);
            VVM = MediatorForVM.Instance;
            VVM.LineChart = this;
        }

        private bool True()
        {
            return true;
        }

        private void RemovePointsOnClick()
        {
            //Remove any point from any series and chart will update
            foreach (var series in Series)
            {
                if (series.Values.Count > 0) series.Values.RemoveAt(0);
            }
        }

        private void AddPointsOnClick()
        {
            //Add a point to any series, and chart will update
            var r = new Random();

            foreach (var series in Series)
            {
                series.Values.Add((double)r.Next(0, 15));
            }
        }

        private void RemoveSeriesOnClick()
        {
            //Remove any series
            if (Series.Count > 0) Series.RemoveAt(0);
        }

        private void AddSeriesOnClick()
        {
            //Ad any series to your chart
            var someRandomValues = new ChartValues<double>();

            var r = new Random();
            var count = Series.Count > 0 ? Series[0].Values.Count : 5;

            for (int i = 0; i < count; i++)
            {
                someRandomValues.Add(r.Next(0, 15));
            }

            var someNewSeries = new LineSeries
            {
                Title = "Some Random Series",
                Values = someRandomValues,
                PointRadius = 0
            };

            Series.Add(someNewSeries);
        }

        private void TestOnClick()
        {
            Series.Clear();
        }

        public void SendExpenses(IConnectedExpensesViewModel to, ViewExpensesInfo message)
        {
            throw new NotImplementedException();
        }

        public void NotifyAboutExpenses(ViewExpensesInfo message)
        {
            if (message != null)
            {
                ExpensesSeries.Values.Clear();

                NewExpensesInfos.Add(message);
            }
            else
            {
                foreach (var value in NewExpensesInfos)
                {
                    ExpensesSeries.Values.Add(value.Expenditure);
                    AxisX.Add(value.CostsDate);
                }

                NewExpensesInfos.Clear();
            }
        }

        public ICommand RemovePointsOnClickCommand { get; set; }

        public ICommand AddPointsOnClickCommand { get; set; }

        public ICommand RemoveSeriesOnClickCommand { get; set; }

        public ICommand AddSeriesOnClickCommand { get; set; }

        public ICommand TestOnClickCommand { get; set; }
    }
}
