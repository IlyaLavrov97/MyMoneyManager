using MyMoneyManager.Model;
using MyMoneyManager.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager
{
    public class DatabaseSynchronizer<T> where T : IBusinessObject
    {
        private readonly ClientModule _clientModule = new ClientModule();

        public bool SynchronizeWithDB(List<T> listOfBusinessObject, byte currency = 0)
        {
            if (IsServerConnected())
            {
                Type[] types = typeof(T).GetInterfaces();
                if (types.Where(t => t.Name == "IMoneyElement") != null)
                {
                    List<IBusinessObject> lstOfBO = new List<IBusinessObject>();
                    foreach (var item in listOfBusinessObject)
                    {
                        lstOfBO.Add(item);
                    }
                    _clientModule.SaveMoneyElementsInDb(lstOfBO, currency);
                }
                return true;
            }
            return false;
        }

        public List<T> GetReliableObjectsFromDB()
        {
            List<T> listOfBusinessObject = new List<T>();
            if (IsServerConnected())
            {
                foreach (var item in _clientModule.GetMoneyElementsFromDb(typeof(T), DateTime.MinValue, DateTime.MaxValue))
                {
                    listOfBusinessObject.Add((T)item);
                }
            }
            return listOfBusinessObject;
        }

        private bool IsServerConnected()
        {
            using (var l_oConnection = new SqlConnection(Settings.Default.DBConnect))
            {
                try
                {
                    l_oConnection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }


    }
}
