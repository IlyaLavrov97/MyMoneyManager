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
        private readonly ManagerSevice.ServiceModuleClient _client;

        public Communicator()
        {
            _client = new ManagerSevice.ServiceModuleClient("NetTcpBinding_IServiceModule");
        } 

        public void AddItemsToDb(List<DtoObject> newItems)
        {
            foreach (var item in newItems)
            {
                _client.AddItemToDb(item);
            }
        }

        public void EditItemsInDb(List<DtoObject> chagedItems)
        {
            foreach (var item in chagedItems)
            {
                _client.EditItemInDb(item);
            }
        }

        public void DeleteItemsFromDb(List<DtoObject> deletedItems)
        {
            foreach (var item in deletedItems)
            {
                _client.DeleteItemFromDb(item);
            }
        }

        public List<DtoObject> GetItemsFromDb(int requestId, DateTime time1, DateTime time2, double value1, double value2, string comment, byte expType)
        {
            List<DtoObject> lstOfDto = new List<DtoObject>();
            lstOfDto = _client.GetItemsFromDb(requestId, time1, time2, value1, value2, comment, expType).ToList();
            return lstOfDto;
        }
    }
}
