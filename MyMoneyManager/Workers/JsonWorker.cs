using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using MyMoneyManager.Model;
using MyMoneyManager.Model.Expenses.BusinessObject;
using System.Data.SqlClient;
using MyMoneyManager.Properties;

namespace MyMoneyManager.Workers
{
    public class JsonWorker<T> where T : class, IBusinessObject
    {
        private List<T> listOfBusinessObject;
        private readonly DatabaseSynchronizer<T> _databaseSyncronizer = new DatabaseSynchronizer<T>();

        public void AddElement(T businessObject)
        {
            if (listOfBusinessObject == null)
            {
                GetJsonObjects();
            }
            listOfBusinessObject.Add(businessObject);
            Push();
        }

        public void DeleteElement(T businessObject)
        {
            if (listOfBusinessObject == null)
            {
                GetJsonObjects();
            }
            var removingElement = listOfBusinessObject.SingleOrDefault(el => el.GetId() == businessObject.GetId());
            listOfBusinessObject.Remove(removingElement);
            Push();
        }

        public List<T> GetElements()
        {
            if (listOfBusinessObject == null)
            {
                Pull();
            }
            List<T> ValueList = new List<T>();
            ValueList.AddRange(listOfBusinessObject);
            return ValueList;
        }

        private void Pull()
        {
            listOfBusinessObject = _databaseSyncronizer.GetReliableObjectsFromDB();
            if (listOfBusinessObject.Count == 0)
            {
                GetJsonObjects();
                // TODO: Оповещение о неподключенной БД.
            }
            else
            {
                // TODO: Оповещение о добавленных, удаленных и измененных элементах, если таковые имеются.
            }
        }

        public void SynchronizeWithDB(byte currency = 0)
        {
            GetJsonObjects();
            if(!_databaseSyncronizer.SynchronizeWithDB(listOfBusinessObject, currency))
            {
                // TODO: Оповещение о неподключенной БД.
            }
        }

        private void GetJsonObjects()
        {
            if (File.Exists(string.Format(@"BusinessObject(" + typeof(T).Name + ").json")))
            {
                using (StreamReader file = File.OpenText(string.Format(@"BusinessObject(" + typeof(T).Name + ").json")))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    listOfBusinessObject = (List<T>)serializer.Deserialize(file, typeof(List<T>));
                }
            }
            else
            {
                listOfBusinessObject = new List<T>();
            }
        }

        private void Push()
        {
            using (StreamWriter file = File.CreateText(string.Format(@"BusinessObject(" + typeof(T).Name + ").json")))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, listOfBusinessObject);
            }
        }
    }
}
