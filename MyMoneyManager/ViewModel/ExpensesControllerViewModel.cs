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

namespace MyMoneyManager.ViewModel
{
    public enum ExchangeRateEnum
    {
        RU = 1,
        USD = 2,
        EUR = 3
    }

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
                searchStringContent = value;
                if (value != string.Empty && value != "Поиск...")
                {
                    Search(value);
                }
                else
                {

                }
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
                OnPropertyChanged("ExpensesDiap1");
            }
        }


        private double expensesDiap2;

        public double ExpensesDiap2
        {
            get { return expensesDiap2; }
            set { expensesDiap2 = value; }
        }


        private string selectedExchangeRate;

        public string SelectedExchangeRate
        {
            get { return selectedExchangeRate; }
            set
            {
                selectedExchangeRate = value;
                OnPropertyChanged("SelectedExchangeRate");
            }
        }


        private bool isDateConsider;

        public bool IsDateConsider
        {
            get { return isDateConsider; }
            set
            {
                isDateConsider = value;
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
                fromExpensesDate = value;
                OnPropertyChanged("FromExpensesDate");
            }
        }


        private DateTime toExpensesDate;

        public DateTime ToExpensesDate
        {
            get { return toExpensesDate; }
            set
            {
                toExpensesDate = value;
                OnPropertyChanged("ToExpensesDate");
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

        private ViewExpensesInfo newExpensesInfo { get; set; }

        #endregion

        MediatorForVM VVM;

        public ExpensesControllerViewModel() 
        {
            Init();
            DeleteCommand = new RelayCommand(arg => DeleteExpenses(), x => CanDelete());
            AddCommand = new RelayCommand(arg => AddExpenses(), x => CanAdd());
            EditCommand = new RelayCommand(arg => EditExpenses(), x => CanEdit());
        }

        
        private void Search(string searchingText)
        {
            List<ViewExpensesInfo> SearchedExpenses = ExpensesInfos.Where(expensesInfo => expensesInfo.Comment.Contains(searchingText) || expensesInfo.ExpensesType.ToString().Contains(searchingText)).ToList();
            ExpensesInfos.Clear();
            foreach (ViewExpensesInfo exp in SearchedExpenses)
            {
                ExpensesInfos.Add(exp);
            }
        }


        #region DeleteExpenses

        private void DeleteExpenses()
        {
            var deleteItem = ExpensesInfos.FirstOrDefault(exp => exp.Id == SelectedExpensesInfo.Id);
            ExpensesInfos.Remove(deleteItem);
            OnPropertyChanged("ExpensesInfos");
        }

        private bool CanDelete()
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
                ViewExpensesInfo editExpensesInfo = new ViewExpensesInfo(SelectedExpensesInfo.Id, SelectedExpensesInfo.Expenditure, SelectedExpensesInfo.Comment, SelectedExpensesInfo.СostsDate, SelectedExpensesInfo.ExpensesType);
                SendExpenses(editExpensesInfo);
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
            VVM.ExpensesController = this;
            SearchStringContent = "Поиск...";
            if (FromExpensesDate == DateTime.MinValue || FromExpensesDate == null)
            {
                FromExpensesDate = DateTime.Now;
            }
            if (ToExpensesDate == DateTime.MinValue || ToExpensesDate == null)
            {
                ToExpensesDate = DateTime.Now;
            }
            if (SelectedExchangeRate == null)
            {
                SelectedExchangeRate = ExchangeRateEnum.RU.ToString();
            }
        }

        public void SendExpenses(ViewExpensesInfo message)
        {
            VVM.SendExpensesTo(VVM.AddOrEditExpenses, message);
        }

        public void NotifyAboutExpenses(ViewExpensesInfo message)
        {
            if (message != null)
            {
                if (TabItemName == "Редактирование расхода")
                {
                    ExpensesInfos.Add(message);
                    ExpensesInfos.Remove(SelectedExpensesInfo);
                    SelectedExpensesInfo = message;
                    TabVisibility = false;
                    DisplayXamlTab = false;
                }
                else
                {
                    ExpensesInfos.Add(message);
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
