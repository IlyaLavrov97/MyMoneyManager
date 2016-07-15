using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Model.Expenses
{
    public enum ExpensesType
    {
        [Description("Любой")]
        Any = 0,
        [Description("Транспорт")]
        Transport = 1,
        [Description("Еда")]
        Food = 2,
        [Description("Здоровье")]
        Health = 3,
        [Description("Развлечения")]
        Entertainment = 4,
        [Description("Другое")]
        Other = 5
    }
}
