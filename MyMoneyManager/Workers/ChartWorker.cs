using MyMoneyManager.Model.Expenses.ViewObject;
using MyMoneyManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Workers
{
    public static class ChartWorker
    {
        public static bool isLastObjForChart;
        //public static void ChartContextChanged(IEnumerable<IViewElement> listOfViewObj)
        //{
        //    List<IViewElement> orderedColl = new List<IViewElement>();

        //    foreach (var item in listOfViewObj)
        //    {
        //        orderedColl.Add(item.Clone());
        //    }

        //    orderedColl = orderedColl.Where(exp => (DateTime.Compare(DateTime.Parse(exp.CostsDate), FromExpensesDate) >= 0)
        //            && (DateTime.Compare(DateTime.Parse(exp.CostsDate), ToExpensesDate) <= 0)).OrderBy(exp => DateTime.Parse(exp.CostsDate)).ToList();


        //    foreach (var exp in orderedColl)
        //    {
        //        SendExpenses(VVM.LineChart, exp);
        //        SendExpenses(VVM.PieChart, exp);
        //    }

        //    SendExpenses(VVM.LineChart, null);
        //    SendExpenses(VVM.PieChart, null);
        //}
    }
}
