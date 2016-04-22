using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMoneyManager.Model.Expenses.ViewObject;

namespace MyMoneyManager.ViewModel.ClassesForVM.Mediator
{
    public interface IMediator
    {
        void SendExpensesTo(IConnectedExpensesViewModel vm, ViewExpensesInfo obj);
    }
}
