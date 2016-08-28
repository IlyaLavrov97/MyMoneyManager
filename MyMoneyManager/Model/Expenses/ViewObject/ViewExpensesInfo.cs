using MyMoneyManager.Model.Expenses.BusinessObject;
using MyMoneyManager.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Model.Expenses.ViewObject
{
    public class ViewExpensesInfo : IViewElement
    {

        #region Свойства

        ///<summary>
        /// Id затраты.
        /// </summary>
        public Guid Id { get; set; }

        ///<summary>
        /// Величина затраты.
        ///<summary>
        public double Expenditure { get; set; }

        ///<summary>
        /// Комментарий к текущей затрате.
        ///<summary>
        public string Comment { get; set; }

        ///<summary>
        /// Дата, когда была осуществленна трата.
        ///<summary>
        public string CostsDate { get; set; }

        /// <summary>
        /// Тип затраты.
        /// </summary>
        public string ExpensesType { get; set; }
        #endregion

        public ViewExpensesInfo(double expenditure, string comment, string costsDate, string expensesType)
        {
            Id = Guid.NewGuid();
            Expenditure = expenditure;
            Comment = comment;
            CostsDate = costsDate;
            ExpensesType = expensesType;
        }

        public ViewExpensesInfo(Guid id, double expenditure, string comment, string costsDate, string expensesType)
        {
            Id = id;
            Expenditure = expenditure;
            Comment = comment;
            CostsDate = costsDate;
            ExpensesType = expensesType;
        }

        public IMoneyElement ConvertToBO()
        {
            return new ExpensesInfo(Id,Expenditure, Comment, DateTime.Parse(CostsDate), (ExpensesType)Enum.Parse(typeof(ExpensesType), EnumWorker.GetValueFromDescription(ExpensesType) == 0 ? ExpensesType : EnumWorker.GetValueFromDescription(ExpensesType).ToString()));
        }

        public Guid GetId()
        {
            return Id;
        }

        public IViewElement Clone()
        {
            return new ViewExpensesInfo(Id, Expenditure, Comment, CostsDate, ExpensesType);
        }

        public double GetMoneyAmount()
        {
            return Expenditure;
        }

        public string GetOperationDate()
        {
            return CostsDate;
        }
    }
}

