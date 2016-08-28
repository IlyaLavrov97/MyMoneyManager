using MyMoneyManager.Infrastucture.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Server
{
    public class ServerModule
    {
        private readonly DatabaseWorker _dataBaseWorker;
        private readonly Converter _converter;

        public ServerModule()
        {
            _dataBaseWorker = new DatabaseWorker();
            _converter = new Converter();
        }

        public void AddItemToDb(IDtoObject item)
        {
            if (item is ExpensesDto)
            {
                _dataBaseWorker.AddExpensesToDb(_converter.ConvertDtoToExpenses(item as ExpensesDto));
            }
        }

        public void DeleteItemFromDb(IDtoObject item)
        {
            if (item is ExpensesDto)
            {
                _dataBaseWorker.DeleteExpensesFromDb(_converter.ConvertDtoToExpenses(item as ExpensesDto));
            }
        }

        public void EditItemInDb(IDtoObject item)
        {
            if (item is ExpensesDto)
            {
                _dataBaseWorker.DeleteExpensesFromDb(_converter.ConvertDtoToExpenses(item as ExpensesDto));
                _dataBaseWorker.AddExpensesToDb(_converter.ConvertDtoToExpenses(item as ExpensesDto));
            }
        }

        public List<IDtoObject> GetItemsFromDb(int requestId, DateTime time1, DateTime time2, double value1, double value2, string comment, byte expType)
        {
            List<IDtoObject> lstOfDto = new List<IDtoObject>();
            switch (requestId)
            {
                case 1:
                    {
                        var lstOfExp = _dataBaseWorker.GetExpensesFromDb(time1, time2, value1, value2, comment, expType);
                        foreach (var item in lstOfExp)
                        {
                            lstOfDto.Add(_converter.ConvertExpensesToDto(item));
                        }
                        break;
                    }
                default:
                    break;
            }
            return lstOfDto;
        }
    }
}
