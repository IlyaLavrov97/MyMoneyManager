using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Infrastucture.DataTransferObjects
{
    [DataContract]
    [KnownType(typeof(ExpensesDto))]
    public abstract class DtoItem : DtoObject
    {
        [DataMember]
        public double AmountOfMoney { get; private set; }

        [DataMember]
        public string Comment { get; private set; }

        [DataMember]
        public DateTime DateOfOperation { get; private set; }

        [DataMember]
        public byte Currency { get; private set; }

        public DtoItem(Guid id, double amountOfMoney, string comment, DateTime dateOfOperation, byte currency) : base(id)
        {
            AmountOfMoney = amountOfMoney;
            Comment = comment;
            DateOfOperation = dateOfOperation;
            Currency = currency;
        }
    }
}
