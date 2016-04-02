using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Model
{
    public class ExpensesInfo
    {

        #region Свойства
        ///<summary>
        /// Величина затраты.
        ///<summary>
        public double Expenditure { get; private set; }

        ///<summary>
        /// Комментарий к текущей затрате.
        ///<summary>
        public string Comment { get; private set; }

        ///<summary>
        /// Дата, когда была осуществленна трата.
        ///<summary>
        public DateTime СostsDate { get; private set; }

        /// <summary>
        /// Тип затраты.
        /// </summary>
        public ExpensesType ExpensesType { get; private set; }
        #endregion

        public ExpensesInfo(double expenditure, string comment, DateTime costsDate, ExpensesType expensesType)
        {
            Expenditure = expenditure;
            Comment = comment;
            СostsDate = costsDate;
            ExpensesType = expensesType;
        }
    }
}
