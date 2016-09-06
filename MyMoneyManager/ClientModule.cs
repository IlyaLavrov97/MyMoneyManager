using System;
using MyMoneyManager.Model;
using MyMoneyManager.Infrastucture;
using MyMoneyManager.Infrastucture.DataTransferObjects;
using MyMoneyManager.Model.Expenses.BusinessObject;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMoneyManager.Workers;
using MyMoneyManager.Model.Expenses;

namespace MyMoneyManager
{
    public class ClientModule
    {
        private readonly Interlayer _interlayer;
        private readonly Communicator<IBusinessObject> _communicator;

        private byte currency;
        public byte Currency
        {
            get
            {
                return currency;
            }
            set
            {
                currency = value;
            }
        }

        public ClientModule()
        {
            _interlayer = new Interlayer();
            _communicator = new Communicator<IBusinessObject>();
        }

        public void SaveMoneyElementsInDb(List<IBusinessObject> newMoneyElements, byte currency)
        {
            Currency = currency;

            List<DtoObject> addedElements = new List<DtoObject>();
            List<DtoObject> changedElements = new List<DtoObject>();
            List<DtoObject> deletedElements = new List<DtoObject>();

            _interlayer.SplitNewElements(ConvertCollToDto(newMoneyElements), out addedElements, out changedElements, out deletedElements);

            if (addedElements.Count != 0)
            {
                AddItemsToDb(addedElements);
            }

            if (changedElements.Count != 0)
            {
                EditItemsInDb(changedElements);
            }

            if (deletedElements.Count != 0)
            {
                DeleteItemsFromDb(deletedElements);
            }
            
        }

        public List<IBusinessObject> GetMoneyElementsFromDb(Type type, DateTime time1, DateTime time2, double value1 = 0, double value2 = double.MaxValue, string comment = null, byte expType = 0)
        {
            List<DtoObject> newDtoLst = new List<DtoObject>();
            newDtoLst = _communicator.GetItemsFromDb(typeToInt(type), time1, time2, value1, value2, comment, expType);
            _interlayer.SetElementsFromDb(newDtoLst);
            List<IBusinessObject> lstOfMoneyElement = ConvertCollToBO(newDtoLst);
            return lstOfMoneyElement;
        }

        private List<DtoObject> ConvertCollToDto(List<IBusinessObject> newColl)
        {
            List<DtoObject> lstDtoItems = new List<DtoObject>();
            foreach (IMoneyElement item in newColl)
            {
                lstDtoItems.Add(item.ConvertToDTO(Currency));
            }
            return lstDtoItems;
        }

        private List<IBusinessObject> ConvertCollToBO(List<DtoObject> newColl)
        {
            List<IBusinessObject> lstDtoItems = new List<IBusinessObject>();
            foreach (DtoObject item in newColl)
            {
                if (item is ExpensesDto)
                {
                    ExpensesDto dto = (ExpensesDto)item;
                    ExpensesInfo newExp = new ExpensesInfo(dto.Id,
                        MoneyWorker.ChangeCurrency(dto.AmountOfMoney,
                        Enum.Parse(typeof(ExchangeRateEnum), ((ExchangeRateEnum)dto.Currency).ToString()).ToString(), 
                        DefaultValuesForControllers.Instance.DefaultExchangeRate, 
                        DefaultValuesForControllers.Instance.DefaultDecimals), 
                        dto.Comment, dto.DateOfOperation, (ExpensesType)dto.ExpensesType);
                    lstDtoItems.Add(newExp);
                }
                
            }
            return lstDtoItems;
        }

        private int typeToInt(Type t)
        {
            switch (t.Name)
            {
                case "ExpensesInfo":
                    {
                        return 1;
                    }
                default:
                    break;
            }
            return 0;
        }

        private void AddItemsToDb(List<DtoObject> newItems)
        {
            _communicator.AddItemsToDb(newItems);
        }

        private void EditItemsInDb(List<DtoObject> changedItems)
        {
            _communicator.EditItemsInDb(changedItems);
        }

        private void DeleteItemsFromDb(List<DtoObject> deletedItems)
        {
            _communicator.DeleteItemsFromDb(deletedItems);
        }
    }
}
