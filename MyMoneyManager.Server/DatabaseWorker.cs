using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMoneyManager.Server.Properties;

namespace MyMoneyManager.Server
{
    public class DatabaseWorker
    {
        ManagerDbContext context = new ManagerDbContext();

        public void AddExpensesToDb(Expenses newExpenses)
        {
            context.Expenses.Add(newExpenses);
            context.SaveChanges();
        }

        public void DeleteExpensesFromDb(Expenses deletedExpenses)
        {
            Expenses delExp = context.Expenses.Find(deletedExpenses.ExpensesId);
            context.Expenses.Remove(delExp);
            context.SaveChanges();
        }

        public List<Expenses> GetExpensesFromDb(DateTime time1, DateTime time2, double value1, double value2, string comment, byte expType)
        {
            List<Expenses> newLst = new List<Expenses>();
            Guid myGuid = Guid.Parse("a4debc71-7754-4d36-8613-994792d54f61");
                newLst = (from exp in context.Expenses
                          where exp.UserId == myGuid && // TODO
                          ((time1 != DateTime.MinValue) ? exp.СostsDate.CompareTo(time1) >= 0 : true) &&
                          ((time1 != DateTime.MaxValue) ? exp.СostsDate.CompareTo(time2) <= 0 : true) &&
                          ((value1 != 0) ? exp.Expenditure >= value1 : true) &&
                          ((value2 != double.MaxValue) ? exp.Expenditure <= value2 : true) &&
                          ((comment != null) ? exp.Comment.Contains(comment) : true) &&
                          ((expType != 0) ? exp.ExpensesType == expType : true)
                          select exp).ToList();
            return newLst;
        }
    }
}
