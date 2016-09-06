using System;
using System.Windows.Input;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyMoneyManager.ViewModel.ClassesForVM;
using MyMoneyManager.Model.Expenses.ViewObject;
using MyMoneyManager.Model.Expenses;
using MyMoneyManager.ViewModel.ClassesForVM.Mediator;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyMoneyManager.Model.Expenses.BusinessObject;
using MyMoneyManager.Model;
using MyMoneyManager.Workers;
using System.ComponentModel;

namespace MyMoneyManager.ViewModel
{
    public class AddOrEditExpensesViewModel : ViewModelBase,
        IConnectedViewModel
    {

        private double newExpensesValue;

        public double NewExpensesValue
        {
            get { return newExpensesValue; }
            set
            {
                newExpensesValue = value;
                OnPropertyChanged("NewExpensesValue");
            }
        }

        private ExpensesType newExpensesType;

        public ExpensesType NewExpensesType
        {
            get { return newExpensesType; }
            set
            {
                newExpensesType = value;
                OnPropertyChanged("NewExpensesType");
            }
        }


        private DateTime newExpensesDate;

        public DateTime NewExpensesDate
        {
            get { return newExpensesDate; }
            set
            {
                newExpensesDate = value;
                OnPropertyChanged("NewExpensesDate");
            }
        }

        private string newExpensesComment;

        public string NewExpensesComment
        {
            get { return newExpensesComment; }
            set
            {
                newExpensesComment = value;
                OnPropertyChanged("NewExpensesComment");
            }
        }

        private Guid newExpensesId;

        public Guid NewExpensesId
        {
            get { return newExpensesId; }
            set
            {
                newExpensesId = value;
            }
        }

        MediatorForVM VVM;

        private ExpensesInfo oldMoneyElement;

        private readonly JsonWorker<ExpensesInfo> _jsonWorker;

        public AddOrEditExpensesViewModel()
        {
            _jsonWorker = new JsonWorker<ExpensesInfo>();
            Init();
        }

        #region SaveExpenses

        private void SaveExpenses()
        {
                if (NewExpensesId != Guid.Empty)
                {
                    ViewExpensesInfo exp = new ViewExpensesInfo(NewExpensesId, NewExpensesValue, NewExpensesComment, NewExpensesDate.ToShortDateString(), NewExpensesType.ToString());
                    _jsonWorker.DeleteElement(oldMoneyElement);
                    Send(VVM.ExpensesController,exp);
                }
                else
                {
                    ViewExpensesInfo exp = new ViewExpensesInfo(NewExpensesValue, NewExpensesComment, NewExpensesDate.ToShortDateString(), NewExpensesType.ToString());
                    Send(VVM.ExpensesController,exp);
                    
                }
        }

        private bool CanSave()
        {
            if (NewExpensesDate != null && NewExpensesValue != 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Cancel

        private void Cancel()
        {
            Send(VVM.ExpensesController, null);
        }

        private bool CanCancel()
        {
            return true;
        }

        #endregion

        private void Init()
        {
            SaveCommand = new RelayCommand(arg => SaveExpenses(), x => CanSave());
            CancelCommand = new RelayCommand(arg => Cancel(), x => CanCancel());
            VVM = MediatorForVM.Instance;
            VVM.AddOrEditExpenses = this;
            if (NewExpensesDate == DateTime.MinValue || NewExpensesDate == null)
            {
                NewExpensesDate = DateTime.Now;
            }
            NewExpensesType = ExpensesType.Other;
        }

        private void Reset()
        {
            NewExpensesDate = DateTime.Now;
            NewExpensesType = ExpensesType.Other;
            NewExpensesId = Guid.Empty;
            NewExpensesComment = string.Empty;
            NewExpensesValue = 0;
        }

        public void Send(IConnectedViewModel to, IViewElement message)
        {
            if (message != null)
            {
                _jsonWorker.AddElement((ExpensesInfo)message.ConvertToBO());
            }

            VVM.SendTo(to, message);

            Reset();
        }

        public void NotifyAbout(IViewElement message)
        {
            ViewExpensesInfo expensesInfo = (ViewExpensesInfo)message;
            NewExpensesId = expensesInfo.Id;
            NewExpensesValue = expensesInfo.Expenditure;
            NewExpensesType = EnumWorker.GetValueFromDescription(expensesInfo.ExpensesType);
            NewExpensesDate = DateTime.Parse(expensesInfo.CostsDate);
            NewExpensesComment = expensesInfo.Comment;
            oldMoneyElement = (ExpensesInfo)message.ConvertToBO();
        }
        

        ///<summary>
        /// Get or set SaveCommand
        /// </summary>
        public ICommand SaveCommand { get; set; }

        ///<summary>
        /// Get or set CancelCommand
        /// </summary>
        public ICommand CancelCommand { get; set; }
    }
}
