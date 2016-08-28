using MyMoneyManager.Infrastucture.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Server
{
    class Converter
    {
        public Expenses ConvertDtoToExpenses(ExpensesDto exp)
        {
            return new Expenses()
            {
                ExpensesId = exp.Id,
                Expenditure = exp.Expenditure,
                CurrencyType = exp.Currency,
                Comment = exp.Comment,
                ExpensesType = exp.ExpensesType,
                СostsDate = exp.CostsDate,
                UserId = Guid.Parse("a4debc71-7754-4d36-8613-994792d54f61") // TODO Установить передачу айди
            };
        }

        public ExpensesDto ConvertExpensesToDto(Expenses exp)
        {
            return new ExpensesDto(exp.ExpensesId, exp.Expenditure, exp.Comment, exp.СostsDate, exp.ExpensesType, exp.CurrencyType);
        }
    }
}
