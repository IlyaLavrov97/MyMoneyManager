using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMoneyManager.Model.Expenses.ViewObject;
using MyMoneyManager.Model.Expenses;
using System.ComponentModel;

namespace MyMoneyManager.ViewModel.ClassesForVM.Searchers
{
    public class LambdaExpensesSearcher : IExpensesSearcher
    {
        public IEnumerable<ViewExpensesInfo> Search(string searchingText, DateTime fromExpensesDate, DateTime toExpensesDate, bool isDateConsider, ExpensesType selectedExpensesType, double expensesDiap1, double expensesDiap2, IEnumerable<ViewExpensesInfo> lst )
        {
            List<ViewExpensesInfo> SearchedExpenses = new List<ViewExpensesInfo>();

            foreach (ViewExpensesInfo item in lst)
            {
                SearchedExpenses.Add((ViewExpensesInfo)item.Clone());
            }

            if (searchingText != null && searchingText != string.Empty && searchingText != DefaultValuesForControllers.Instance.DefaultSearchStringContent)
            {
                SearchedExpenses = SearchedExpenses.Where(expensesInfo => expensesInfo.Comment.IndexOf(searchingText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            if (isDateConsider)
            {
                SearchedExpenses = SearchedExpenses.Where(expensesInfo => (DateTime.Compare(DateTime.Parse(expensesInfo.CostsDate), fromExpensesDate) >= 0) && (DateTime.Compare(DateTime.Parse(expensesInfo.CostsDate), toExpensesDate) <= 0)).ToList();
            }

            if (selectedExpensesType != ExpensesType.Any)
            {
                SearchedExpenses = SearchedExpenses.Where(expensesInfo => expensesInfo.ExpensesType == selectedExpensesType.GetType()
                                                    .GetMember(selectedExpensesType.ToString())[0]
                                                    .GetCustomAttributes(true)
                                                    .OfType<DescriptionAttribute>()
                                                    .First()
                                                    .Description).ToList();
            }

            SearchedExpenses = SearchedExpenses.Where(expensesInfo => expensesInfo.Expenditure >= expensesDiap1 && expensesInfo.Expenditure <= expensesDiap2).ToList();

            List<ViewExpensesInfo> expensesInfos = new List<ViewExpensesInfo>(); 
            foreach (ViewExpensesInfo exp in SearchedExpenses)
            {
                exp.Expenditure = Math.Round(exp.Expenditure, DefaultValuesForControllers.Instance.DefaultDecimals);
                expensesInfos.Add(exp);
            }

            return expensesInfos;
        }
    }
}
