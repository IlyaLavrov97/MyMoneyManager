using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMoneyManager.Model.Expenses.ViewObject;

namespace MyMoneyManager.ViewModel.ClassesForVM.Mediator
{
    public interface IConnectedExpensesViewModel
    {
        void SendExpenses(IConnectedExpensesViewModel to,  ViewExpensesInfo message);

        void NotifyAboutExpenses(ViewExpensesInfo message);
    }
}
