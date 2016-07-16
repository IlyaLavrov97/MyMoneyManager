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

namespace MyMoneyManager.ViewModel
{

    public class ExpensesControllerViewModel : ViewModelBase, 
        IMainTabItem,
        IConnectedExpensesViewModel
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
                    SearchV1(value, ExpensesInfos);
                }
                else if(initIsComplete)
                {
                    SearchV1(value, listOfViewObj);
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
            get { return expensesDiap1; }
            set
            {
                expensesDiap1 = value;
                if (initIsComplete)
                {
                    SearchV1(SearchStringContent, listOfViewObj);
                }
                OnPropertyChanged("ExpensesDiap1");
            }
        }


        private double expensesDiap2;

        public double ExpensesDiap2
        {
            get { return expensesDiap2; }
            set
            {
                expensesDiap2 = value;
                if (initIsComplete)
                {
                    SearchV1(SearchStringContent, listOfViewObj);
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
                        SetNewExchangeRateInColl(listOfViewObj, PreviousExchangeRate, SelectedExchangeRate, 10);
                        SearchV1(SearchStringContent, listOfViewObj);
                        SetMaxExpensesValue();
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
                SearchV1(SearchStringContent, listOfViewObj);
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
                        SearchV1(SearchStringContent, listOfViewObj);
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
                        SearchV1(SearchStringContent, listOfViewObj);
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
                    SearchV1(SearchStringContent, listOfViewObj);
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

        private MediatorForVM VVM;
        private DefaultValuesForControllers defaultValues;
        private bool initIsComplete = false;


        public ExpensesControllerViewModel()
        {
            Init();
            DeleteCommand = new RelayCommand(arg => DeleteExpenses(), x => CanDelete());
            AddCommand = new RelayCommand(arg => AddExpenses(), x => CanAdd());
            EditCommand = new RelayCommand(arg => EditExpenses(), x => CanEdit());
        }

        private void SearchV1(string searchingText, IEnumerable<ViewExpensesInfo> lst)
        {
            List<ViewExpensesInfo> SearchedExpenses = new List<ViewExpensesInfo>();

            foreach (ViewExpensesInfo item in lst)
            {
                SearchedExpenses.Add(item.Clone());
            }

            if (searchingText != null && searchingText != string.Empty && searchingText != defaultValues.DefaultSearchStringContent)
            {
                SearchedExpenses = SearchedExpenses.Where(expensesInfo => expensesInfo.Comment.IndexOf(searchingText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            if (IsDateConsider)
            {
                SearchedExpenses = SearchedExpenses.Where(expensesInfo => (DateTime.Compare(DateTime.Parse(expensesInfo.CostsDate), FromExpensesDate) >= 0) && (DateTime.Compare(DateTime.Parse(expensesInfo.CostsDate), ToExpensesDate) <= 0)).ToList();
            }

            if (SelectedExpensesType != ExpensesType.Any)
            {
                SearchedExpenses = SearchedExpenses.Where(expensesInfo => expensesInfo.ExpensesType == SelectedExpensesType.GetType()
                                                    .GetMember(SelectedExpensesType.ToString())[0]
                                                    .GetCustomAttributes(true)
                                                    .OfType<DescriptionAttribute>()
                                                    .First()
                                                    .Description).ToList();
            }

            SearchedExpenses = SearchedExpenses.Where(expensesInfo => expensesInfo.Expenditure >= ExpensesDiap1 && expensesInfo.Expenditure <= ExpensesDiap2).ToList();

            ExpensesInfos.Clear();
            foreach (ViewExpensesInfo exp in SearchedExpenses)
            {
                exp.Expenditure = Math.Round(exp.Expenditure, defaultValues.DefaultDecimals);
                ExpensesInfos.Add(exp);
            }

        }
        
        private void ResetMainCollections()
        {
            ExpensesInfos.Clear();
            listOfViewObj = new List<ViewExpensesInfo>();
            var coll = JsonWorker.GetElementsFrom(typeof(ExpensesInfo));
            IViewElement VO;
            foreach (var item in coll)
            {
                MyObjectsConverter.ConvertBOtoVO(item, out VO);
                ViewExpensesInfo newObj = (ViewExpensesInfo)VO;
                ExpensesInfos.Add(newObj);
                listOfViewObj.Add(newObj.Clone());
            }
        }

        private void SetNewExchangeRateInColl(IEnumerable<ViewExpensesInfo> coll, string oldExchangeRate, string newExchangeRate, int decimals)
        {
            foreach (ViewExpensesInfo item in coll)
            {
                item.Expenditure = Math.Round((item.Expenditure * MoneyWorker.GetExchangeRateValue((ExchangeRateEnum)Enum.Parse(typeof(ExchangeRateEnum), oldExchangeRate)))
                    / MoneyWorker.GetExchangeRateValue((ExchangeRateEnum)Enum.Parse(typeof(ExchangeRateEnum), newExchangeRate)), decimals);
            }
        }


        #region DeleteExpenses

        private void DeleteExpenses()
        {
            var deleteItem = listOfViewObj.FirstOrDefault(exp => exp.Id == SelectedExpensesInfo.Id);
            listOfViewObj.Remove(deleteItem);
            ExpensesInfos.Remove(SelectedExpensesInfo);
            IMoneyElement newBO;
            MyObjectsConverter.ConvertVOtoBO(deleteItem, out newBO);
            JsonWorker.DeleteElement(newBO);
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
                ExpensesDiap2 = Math.Round(expLst.Max(), defaultValues.DefaultDecimals);
            }
        }

        private void ChartContextChanged()
        {
            List<ViewExpensesInfo> orderedColl = new List<ViewExpensesInfo>();

            foreach (var item in listOfViewObj)
            {
                orderedColl.Add(item.Clone());
            }

            orderedColl = orderedColl.Where(exp => (DateTime.Compare(DateTime.Parse(exp.CostsDate), FromExpensesDate) >= 0)
                    && (DateTime.Compare(DateTime.Parse(exp.CostsDate), ToExpensesDate) <= 0)).OrderBy(exp => DateTime.Parse(exp.CostsDate)).ToList();

            
            foreach (var exp in orderedColl)
            {
                SendExpenses(VVM.LineChart, exp);
                SendExpenses(VVM.PieChart, exp);
            }

            SendExpenses(VVM.LineChart, null);
            SendExpenses(VVM.PieChart, null);
        }

        

        public void SendExpenses(IConnectedExpensesViewModel to, ViewExpensesInfo message)
        {
            VVM.SendExpensesTo(to, message);
        }

        public void NotifyAboutExpenses(ViewExpensesInfo message)
        {
            if (message != null)
            {
                message.ExpensesType = EnumWorker.GetDescriptionFromValue(message.ExpensesType);
                if (TabItemName == "Редактирование расхода")
                {
                    ExpensesInfos.Add(message);
                    listOfViewObj.Add(message);
                    var deleteItem = listOfViewObj.FirstOrDefault(exp => exp.Id == SelectedExpensesInfo.Id);
                    listOfViewObj.Remove(deleteItem);
                    ExpensesInfos.Remove(SelectedExpensesInfo);
                    SelectedExpensesInfo = message;
                    SetMaxExpensesValue();
                    TabVisibility = false;
                    DisplayXamlTab = false;
                }
                else
                {
                    ExpensesInfos.Add(message);
                    listOfViewObj.Add(message);
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

        #endregion

    }
}
