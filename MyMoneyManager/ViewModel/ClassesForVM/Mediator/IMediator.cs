using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMoneyManager.Model.Expenses.ViewObject;
using MyMoneyManager.Model;

namespace MyMoneyManager.ViewModel.ClassesForVM.Mediator
{
    public interface IMediator
    {
        void SendExpensesTo(IConnectedViewModel to, IViewElement obj);
    }
}
