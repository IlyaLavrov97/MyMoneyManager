using System;
using System.Windows.Input;
using Microsoft.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MyMoneyManager.Model.Expenses;
using MyMoneyManager.Model.Expenses.ViewObject;
using MyMoneyManager.ViewModel.ClassesForVM;
using MyMoneyManager.ViewModel.ClassesForVM.Mediator;
using MyMoneyManager.Model;
using MyMoneyManager.Model.Expenses.BusinessObject;
using MyMoneyManager.Workers;
using System.ComponentModel;
using MyMoneyManager.ViewModel.ClassesForVM.Searchers;

namespace MyMoneyManager.ViewModel
{

    public class ExpensesControllerViewModel : ViewModelBase, 
        IMainTabItem,
        IConnectedViewModel
    {

        #region Properties

        private ObservableCollection<ViewExpensesInfo> expensesInfos = new ObservableCollection<ViewExpensesInfo>();

        public ObservableCollection<ViewExpensesInfo> ExpensesInfos
        {
            get { return expensesInfos; }
            set
            {
                expensesInfos = value;
                OnPropertyChanged("ExpensesInfos");
            }
        }


        private string searchStringContent;

        public string SearchStringContent
        {
            get { return searchStringContent; }
            set
            {
                if (initIsComplete && searchStringContent.Count() < value.Count())
                {
                    Search(value, ExpensesInfos);
                }
                else if(initIsComplete)
                {
                    Search(value, listOfViewObj);
                }
                searchStringContent = value;
                OnPropertyChanged("SearchStringContent");
            }
        }


        private ViewExpensesInfo selectedExpensesInfo;

        public ViewExpensesInfo SelectedExpensesInfo
        {
            get { return selectedExpensesInfo; }
            set
            {
                selectedExpensesInfo = value;
                if (selectedExpensesInfo != null)
                {
                    SelectedItemComment = selectedExpensesInfo.Comment;
                    IsExpanded = true;
                }
                else
                {
                    SelectedItemComment = string.Empty;
                    IsExpanded = false;
                }
                OnPropertyChanged("SelectedExpensesInfo");
            }
        }


        private string selectedItemComment;

        public string SelectedItemComment
        {
            get { return selectedItemComment; }
            set
            {
                selectedItemComment = value;
                OnPropertyChanged("SelectedItemComment");
            }
        }


        private bool isExpanded;

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }


        private double expensesDiap1;

        public double ExpensesDiap1
        {
            get { return Math.Round(expensesDiap1, DefaultValuesForControllers.Instance.DefaultDecimals); }
            set
            {
                expensesDiap1 = value;
                if (initIsComplete)
                {
                    Search(SearchStringContent, listOfViewObj);
                }
                OnPropertyChanged("ExpensesDiap1");
            }
        }


        private double expensesDiap2;

        public double ExpensesDiap2
        {
            get { return Math.Round(expensesDiap2, DefaultValuesForControllers.Instance.DefaultDecimals); }
            set
            {
                expensesDiap2 = value;
                if (initIsComplete)
                {
                    Search(SearchStringContent, listOfViewObj);
                }
                OnPropertyChanged("ExpensesDiap2");
            }
        }


        private string selectedExchangeRate;

        public string SelectedExchangeRate
        {
            get { return selectedExchangeRate; }
            set
            {
                if (selectedExchangeRate != value)
                {
                    PreviousExchangeRate = SelectedExchangeRate;
                    selectedExchangeRate = value;
                    if (initIsComplete)
                    {
                        MoneyWorker.SetNewExchangeRateInColl(listOfViewObj, PreviousExchangeRate, SelectedExchangeRate, 10);
                        expensesDiap1 = MoneyWorker.ChangeCurrency(expensesDiap1 , PreviousExchangeRate, SelectedExchangeRate, 10);
                        OnPropertyChanged("ExpensesDiap1");
                        expensesDiap2 = MoneyWorker.ChangeCurrency(expensesDiap2 , PreviousExchangeRate, SelectedExchangeRate, 10);
                        OnPropertyChanged("ExpensesDiap2");
                        Search(SearchStringContent, listOfViewObj);
                    }
                    OnPropertyChanged("SelectedExchangeRate");
                }
            }
        }


        private string previousExchangeRate;

        public string PreviousExchangeRate
        {
            get { return previousExchangeRate; }
            set
            {
                previousExchangeRate = value;
            }
        }


        private bool isDateConsider;

        public bool IsDateConsider
        {
            get { return isDateConsider; }
            set
            {
                isDateConsider = value;
                Search(SearchStringContent, listOfViewObj);
                OnPropertyChanged("IsDateConsider");
            }
        }
        

        private DateTime fromExpensesDate;

        public DateTime FromExpensesDate
        {
            get
            {
                return fromExpensesDate;
            }
            set
            {
                if (fromExpensesDate.Date != value.Date)
                {
                    fromExpensesDate = value;
                    if (initIsComplete)
                    {
                        Search(SearchStringContent, listOfViewObj);
                        ChartContextChanged();
                    }
                    OnPropertyChanged("FromExpensesDate");
                }
            }

        }


        private DateTime toExpensesDate;

        public DateTime ToExpensesDate
        {
            get { return toExpensesDate; }
            set
            {
                if (toExpensesDate.Date != value.Date)
                {
                    toExpensesDate = value;
                    if (initIsComplete)
                    {
                        Search(SearchStringContent, listOfViewObj);
                        ChartContextChanged();
                    }
                    OnPropertyChanged("ToExpensesDate");
                }
            }
        }


        private ExpensesType selectedExpensesType;

        public ExpensesType SelectedExpensesType
        {
            get { return selectedExpensesType; }
            set
            {
                selectedExpensesType = value;
                if (initIsComplete)
                {
                    Search(SearchStringContent, listOfViewObj);
                }
                OnPropertyChanged("SelectedExpensesType");
            }
        }


        private string tabItemName;

        public string TabItemName
        {
            get { return tabItemName; }
            set
            {
                tabItemName = value;
                OnPropertyChanged("TabItemName");
            }
        }


        private bool tabVisibility;

        public bool TabVisibility
        {
            get { return tabVisibility; }
            set
            {
                tabVisibility = value;
                OnPropertyChanged("TabVisibility");
            }
        }


        private bool displayXamlTab;

        public bool DisplayXamlTab
        {
            get { return displayXamlTab; }
            set
            {
                displayXamlTab = value;
                OnPropertyChanged("DisplayXamlTab");
            }
        }


        private bool isLineChartConsider;

        public bool IsLineChartConsider
        {
            get { return isLineChartConsider; }
            set
            {
                isLineChartConsider = value;
                OnPropertyChanged("IsLineChartConsider");
            }
        }


        private bool isPieChartConsider;

        public bool IsPieChartConsider
        {
            get { return isPieChartConsider; }
            set
            {
                isPieChartConsider = value;
                OnPropertyChanged("IsPieChartConsider");
            }
        }

        
        private ViewExpensesInfo newExpensesInfo { get; set; }

        private List<ViewExpensesInfo> listOfViewObj { get; set; }

        #endregion


        private readonly IExpensesSearcher _searcher;
        private readonly JsonWorker<ExpensesInfo> _jsonWorker;
        private MediatorForVM VVM;
        private DefaultValuesForControllers defaultValues;
        private bool initIsComplete = false;


        public ExpensesControllerViewModel()
        {
            _searcher = new LambdaExpensesSearcher();
            _jsonWorker = new JsonWorker<ExpensesInfo>();
            Init();
        }

        private void Search(string searchingText, IEnumerable<ViewExpensesInfo> lst)
        {
            IEnumerable<ViewExpensesInfo> foundExpensesInfos = _searcher.Search(searchingText, FromExpensesDate,
                ToExpensesDate, IsDateConsider, SelectedExpensesType, expensesDiap1, expensesDiap2, lst);
            ExpensesInfos.Clear();
            foreach (var item in foundExpensesInfos)
            {
                ExpensesInfos.Add(item);
            }
        }

        private void ResetMainCollections()
        {
            ExpensesInfos.Clear();
            listOfViewObj = new List<ViewExpensesInfo>();
            var coll = _jsonWorker.GetElements();
            foreach (ExpensesInfo item in coll)
            {
                ExpensesInfos.Add((ViewExpensesInfo)item.ConvertToVO());
                listOfViewObj.Add((ViewExpensesInfo)item.ConvertToVO().Clone());
            }
        }

        private void SaveExpenses()
        {
            int a = (int)Enum.Parse(typeof(ExchangeRateEnum), selectedExchangeRate);
            byte b = (byte)a;
            _jsonWorker.SynchronizeWithDB(b);
        }

        private bool CanSave()
        {
            return true;
        }

        #region DeleteExpenses

        private void DeleteExpenses()
        {
            var deleteItem = listOfViewObj.FirstOrDefault(exp => exp.Id == SelectedExpensesInfo.Id);
            listOfViewObj.Remove(deleteItem);
            ExpensesInfos.Remove(SelectedExpensesInfo);
            _jsonWorker.DeleteElement((ExpensesInfo)deleteItem.ConvertToBO());
        }

        private bool CanDelete()
        {
            if (TabVisibility || SelectedExpensesInfo == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region AddExpenses

        private void AddExpenses()
        {
            TabItemName = "Добавление расхода";
            TabVisibility = true;
            DisplayXamlTab = true;
        }

        private bool CanAdd()
        {
            if (TabVisibility)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region EditExpenses

        private void EditExpenses()
        {
            if (SelectedExpensesInfo != null)
            {
                TabItemName = "Редактирование расхода";
                TabVisibility = true;
                DisplayXamlTab = true;
                ViewExpensesInfo editExpensesInfo = new ViewExpensesInfo(SelectedExpensesInfo.Id, SelectedExpensesInfo.Expenditure, SelectedExpensesInfo.Comment, SelectedExpensesInfo.CostsDate, SelectedExpensesInfo.ExpensesType);
                SendExpenses(VVM.AddOrEditExpenses, editExpensesInfo);
            }

        }

        private bool CanEdit()
        {
            if (TabVisibility || SelectedExpensesInfo == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        private void Init()
        {
            DeleteCommand = new RelayCommand(arg => DeleteExpenses(), x => CanDelete());
            AddCommand = new RelayCommand(arg => AddExpenses(), x => CanAdd());
            EditCommand = new RelayCommand(arg => EditExpenses(), x => CanEdit());
            SaveCommand = new RelayCommand(arg => SaveExpenses(), x=> CanSave());
            VVM = MediatorForVM.Instance;
            defaultValues = DefaultValuesForControllers.Instance;
            VVM.ExpensesController = this;
            SelectedExchangeRate = defaultValues.DefaultExchangeRate;
            FromExpensesDate = defaultValues.DefaultDateTime;
            ToExpensesDate = defaultValues.DefaultDateTime;
            SearchStringContent = defaultValues.DefaultSearchStringContent;
            SelectedExpensesType = ExpensesType.Any;
            ResetMainCollections();
            SetMaxExpensesValue();
            initIsComplete = true;
        }

        private void SetMaxExpensesValue()
        {
            List<double> expLst = new List<double>();

            foreach (var exp in listOfViewObj)
            {
                expLst.Add(exp.Expenditure);
            }

            if (expLst.Count != 0)
            {
                ExpensesDiap2 = Math.Round(expLst.Max() + 1 / Math.Pow(10, defaultValues.DefaultDecimals), defaultValues.DefaultDecimals);
            }
        }

        private void ChartContextChanged()
        {
            List<ViewExpensesInfo> orderedColl = new List<ViewExpensesInfo>();

            foreach (var item in listOfViewObj)
            {
                ViewExpensesInfo newItem = (ViewExpensesInfo)item.Clone();
                newItem.Expenditure = Math.Round(newItem.Expenditure, DefaultValuesForControllers.Instance.DefaultDecimals);
                orderedColl.Add(newItem);
            }

            orderedColl = orderedColl.Where(exp => (DateTime.Compare(DateTime.Parse(exp.CostsDate), FromExpensesDate) >= 0)
                    && (DateTime.Compare(DateTime.Parse(exp.CostsDate), ToExpensesDate) <= 0)).OrderBy(exp => DateTime.Parse(exp.CostsDate)).ToList();

            for (int i = 0; i < orderedColl.Count; i++)
            {
                if (orderedColl.Count - 1 == i)
                {
                    ChartWorker.isLastObjForChart = true;
                }
                SendExpenses(VVM.LineChart, orderedColl.ToArray()[i]);
                SendExpenses(VVM.PieChart, orderedColl.ToArray()[i]);
            }
            ChartWorker.isLastObjForChart = false;

        }

        

        public void SendExpenses(IConnectedViewModel to, IViewElement message)
        {
            VVM.SendExpensesTo(to, message);
        }

        public void NotifyAboutExpenses(IViewElement message)
        {
            ViewExpensesInfo expensesInfo = (ViewExpensesInfo)message;
            if (expensesInfo != null)
            {
                expensesInfo.ExpensesType = EnumWorker.GetDescriptionFromValue(expensesInfo.ExpensesType);
                if (TabItemName == "Редактирование расхода")
                {
                    ExpensesInfos.Add(expensesInfo);
                    listOfViewObj.Add(expensesInfo);
                    var deleteItem = listOfViewObj.FirstOrDefault(exp => exp.Id == SelectedExpensesInfo.Id);
                    listOfViewObj.Remove(deleteItem);
                    ExpensesInfos.Remove(SelectedExpensesInfo);
                    SelectedExpensesInfo = expensesInfo;
                    SetMaxExpensesValue();
                    TabVisibility = false;
                    DisplayXamlTab = false;
                }
                else
                {
                    ExpensesInfos.Add(expensesInfo);
                    listOfViewObj.Add(expensesInfo);
                    SetMaxExpensesValue();
                    TabVisibility = false;
                    DisplayXamlTab = false;
                }
            }
            else
            {
                TabVisibility = false;
                DisplayXamlTab = false;
            }
        }

        #region Commands



        ///<summary>
        /// Get or set AddCommand
        /// </summary>
        public ICommand AddCommand { get; set; }

        ///<summary>
        /// Get or set DeleteCommand
        /// </summary>
        public ICommand DeleteCommand { get; set; }


        ///<summary>
        /// Get or set EditCommand
        /// </summary>
        public ICommand EditCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        #endregion

    }
}
