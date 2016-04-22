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

namespace MyMoneyManager.ViewModel
{
    public class AddOrEditExpensesViewModel : ViewModelBase,
        IConnectedExpensesViewModel
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

        public AddOrEditExpensesViewModel()
        {
            Init();
            SaveCommand = new RelayCommand(arg => SaveExpenses(), x => CanSave());
            CancelCommand = new RelayCommand(arg => Cancel(), x => CanCancel());
        }

        #region SaveExpenses

        private void SaveExpenses()
        {
                if (NewExpensesId != Guid.Empty)
                {
                    ViewExpensesInfo exp = new ViewExpensesInfo(NewExpensesId, NewExpensesValue, NewExpensesComment, NewExpensesDate.ToShortDateString(), NewExpensesType);
                    SendExpenses(exp);
                }
                else
                {
                    ViewExpensesInfo exp = new ViewExpensesInfo(NewExpensesValue, NewExpensesComment, NewExpensesDate.ToShortDateString(), NewExpensesType);
                    SendExpenses(exp);
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
            SendExpenses(null);
        }

        private bool CanCancel()
        {
            return true;
        }

        #endregion

        private void Init()
        {
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

        public void SendExpenses(ViewExpensesInfo message)
        {
            VVM.SendExpensesTo(VVM.ExpensesController, message);
            Reset();
        }

        public void NotifyAboutExpenses(ViewExpensesInfo message)
        {
            NewExpensesId = message.Id;
            NewExpensesValue = message.Expenditure;
            NewExpensesType = message.ExpensesType;
            NewExpensesDate = DateTime.Parse(message.СostsDate);
            NewExpensesComment = message.Comment;
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
