using System;
using MyMoneyManager.Server;
using MyMoneyManager.Infrastucture.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager
{
    public class Communicator<T>
    {
        private readonly ServerModule _serverModule;
        
        public Communicator()
        {
            _serverModule = new ServerModule();
        } 

        public void AddItemsToDb(List<IDtoObject> newItems)
        {
            foreach (var item in newItems)
            {
                _serverModule.AddItemToDb(item);
            }
        }

        public void EditItemsInDb(List<IDtoObject> chagedItems)
        {
            foreach (var item in chagedItems)
            {
                _serverModule.EditItemInDb(item);
            }
        }

        public void DeleteItemsFromDb(List<IDtoObject> deletedItems)
        {
            foreach (var item in deletedItems)
            {
                _serverModule.DeleteItemFromDb(item);
            }
        }

        public List<IDtoObject> GetItemsFromDb(int requestId, DateTime time1, DateTime time2, double value1, double value2, string comment, byte expType)
        {
            List<IDtoObject> lstOfDto = new List<IDtoObject>();
            lstOfDto = _serverModule.GetItemsFromDb(requestId, time1, time2, value1, value2, comment, expType);
            return lstOfDto;
        }
    }
}
