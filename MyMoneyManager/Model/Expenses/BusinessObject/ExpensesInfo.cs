using MyMoneyManager.Model.Expenses.ViewObject;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MyMoneyManager.Infrastucture.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MyMoneyManager.Workers;

namespace MyMoneyManager.Model.Expenses.BusinessObject
{
    class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            DateTimeFormat = "dd/MM/yyyy";
        }
    }

    public class ExpensesInfo : IMoneyElement
    {

        #region Свойства

        ///<summary>
        /// Id затраты.
        /// </summary>
        public Guid Id { get; private set; }

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
        // TODO не установлен приватный сеттер потому что возникают проблемы при десериализации даты.  
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime СostsDate { get; set; }

        /// <summary>
        /// Тип затраты.
        /// </summary>
        public ExpensesType ExpensesType { get; private set; }
        #endregion

        
        public ExpensesInfo(double expenditure, string comment, DateTime costsDate, ExpensesType expensesType)
        {
            Id = Guid.NewGuid();
            Expenditure = expenditure;
            Comment = comment;
            СostsDate = costsDate;
            ExpensesType = expensesType;
        }

        [JsonConstructor]
        public ExpensesInfo(Guid id, double expenditure, string comment, DateTime costsDate, ExpensesType expensesType)
        {
            Id = id;
            Expenditure = expenditure;
            Comment = comment;
            СostsDate = costsDate;
            ExpensesType = expensesType;
        }

        public IViewElement ConvertToVO()
        {
            return new ViewExpensesInfo(Id, Expenditure, Comment, СostsDate.ToShortDateString(), EnumWorker.GetDescriptionFromValue(ExpensesType.ToString()));
        }

        public DtoObject ConvertToDTO(byte currency)
        {
            return new ExpensesDto(Id, Expenditure, Comment, СostsDate, (byte)ExpensesType, currency);
        }

        public Guid GetId()
        {
            return Id;
        }
    }
}
