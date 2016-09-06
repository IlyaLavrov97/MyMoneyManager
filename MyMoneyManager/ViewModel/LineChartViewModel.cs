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
using MyMoneyManager.Workers;

namespace MyMoneyManager.ViewModel
{
    public class LineChartViewModel : ViewModelBase,
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

        private Dictionary<string, List<IViewElement>> dictionary = new Dictionary<string, List<IViewElement>>();

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

        MediatorForVM VVM;

        public LineChartViewModel()
        {
            Init();
        }

        private void Init()
        {
            VVM = MediatorForVM.Instance;
            VVM.LineChart = this;
        }
        

        public void Send(IConnectedViewModel to, IViewElement message)
        {
            throw new NotImplementedException();
        }

        public void NotifyAbout(IViewElement message)
        {
            bool isNewSeries = true;
            LineSeries oldSerries = new LineSeries();
            foreach (var item in Series)
            {
                if (item.Title == message.GetType().Name)
                {
                    isNewSeries = false;
                    oldSerries = (LineSeries)item;
                    break;
                }
            }

            if (isNewSeries)
            {
                Series.Add(new LineSeries
                {
                    Title = message.GetType().Name,
                    Values = new ChartValues<double>()
                });
                List<IViewElement> newColl = new List<IViewElement>();
                newColl.Add(message);
                dictionary.Add(message.GetType().Name, newColl);
            }
            else
            {
                oldSerries.Values.Clear();
                List<IViewElement> oldColl = dictionary[message.GetType().Name];
                oldColl.Add(message);
                dictionary[message.GetType().Name] = oldColl;
            }


            if (ChartWorker.isLastObjForChart == true)
            {
                foreach (var item in Series)
                {
                    if (item.Title == message.GetType().Name)
                    {
                        oldSerries = (LineSeries)item;
                        break;
                    }
                }
                foreach (var value in dictionary[message.GetType().Name])
                {
                    oldSerries.Values.Add(value.GetMoneyAmount());
                    AxisX.Add(value.GetOperationDate());
                }

                dictionary[message.GetType().Name].Clear();
            }
        }
    }
}
