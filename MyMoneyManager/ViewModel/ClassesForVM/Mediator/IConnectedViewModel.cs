using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMoneyManager.Model.Expenses.ViewObject;
using MyMoneyManager.Model;

namespace MyMoneyManager.ViewModel.ClassesForVM.Mediator
{
    public interface IConnectedViewModel
    {
        void Send(IConnectedViewModel to, IViewElement message);

        void NotifyAbout(IViewElement message);
    }
}
