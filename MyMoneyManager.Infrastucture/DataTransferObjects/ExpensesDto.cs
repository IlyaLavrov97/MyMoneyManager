using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Infrastucture.DataTransferObjects
{
    [DataContract]
    public class ExpensesDto : DtoItem
    {
        [DataMember]
        public byte ExpensesType { get; private set; }
        
        public ExpensesDto(Guid id, double expenditure, string comment, DateTime costsDate, byte expensesType, byte currency) : base(id, expenditure, comment, costsDate, currency)
        {
            ExpensesType = expensesType;
        }

        public override bool IsEqual(DtoObject dtoObj)
        {
            ExpensesDto exp = dtoObj as ExpensesDto;
            if (exp != null && exp.Id == Id && exp.ExpensesType == ExpensesType && exp.DateOfOperation == DateOfOperation && exp.Currency == Currency && exp.Comment == Comment && exp.AmountOfMoney == AmountOfMoney)
            {
                return true;
            }
            return false;
        }
    }
}
