using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Infrastucture.DataTransferObjects
{
    public class ExpensesDto : IDtoItem
    {

        public Guid Id { get; private set; }

        public double Expenditure { get; private set; }

        public string Comment { get; private set; }

        public DateTime CostsDate { get; private set; }

        public byte ExpensesType { get; private set; }

        public byte Currency { get; private set; }
        
        public ExpensesDto(Guid id, double expenditure, string comment, DateTime costsDate, byte expensesType, byte currency)
        {
            Id = id;
            Expenditure = expenditure;
            Comment = comment;
            CostsDate = costsDate;
            ExpensesType = expensesType;
            Currency = currency;
        }

        public Guid GetId()
        {
            return Id;
        }

        public bool IsEqual(IDtoObject item)
        {
            ExpensesDto expDto = item as ExpensesDto;
            if (Expenditure == expDto.Expenditure && Comment == expDto.Comment && CostsDate == expDto.CostsDate && ExpensesType == expDto.ExpensesType && Currency == expDto.Currency)
            {
                return true;
            }
            return false;
        }
    }
}
