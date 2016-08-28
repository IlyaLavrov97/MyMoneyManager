using MyMoneyManager.Model.Expenses;
using MyMoneyManager.Model.Expenses.ViewObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.ViewModel.ClassesForVM.Searchers
{
    public interface IExpensesSearcher
    {
          IEnumerable<ViewExpensesInfo> Search(string searchingText, DateTime fromExpensesDate, DateTime toExpensesDate, bool isDateConsider, ExpensesType selectedExpensesType, double expensesDiap1, double expensesDiap2, IEnumerable<ViewExpensesInfo> lst);
    }
}
