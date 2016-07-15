using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using MyMoneyManager.Model;
using MyMoneyManager.Model.Expenses.BusinessObject;

namespace MyMoneyManager.Workers
{
    public class JsonWorker
    {

        private static List<ExpensesInfo> ExpensesInfosList;
        
        public static void AddElement(IMoneyElement moneyElement)
        {
            if (typeof(ExpensesInfo) == moneyElement.GetType())
            {
                if (ExpensesInfosList == null)
                {
                    Pull(moneyElement.GetType());
                }
                ExpensesInfosList.Add((ExpensesInfo)moneyElement);
            }

            Push(moneyElement.GetType());
        }

        public static void DeleteElement(IMoneyElement moneyElement)
        {
            Pull(moneyElement.GetType());
            if (moneyElement.GetType() == typeof(ExpensesInfo))
            {
                var removingElement = ExpensesInfosList.SingleOrDefault(el => el.Id == moneyElement.GetId());
                ExpensesInfosList.Remove(removingElement);
            }
            Push(moneyElement.GetType());
        }

        public static List<IMoneyElement> GetElementsFrom(Type t)
        {
            if (ExpensesInfosList == null)
            {
                Pull(t);
            }

            List<IMoneyElement> ValueList = new List<IMoneyElement>();

            if (t == typeof(ExpensesInfo))
            {
                ValueList.AddRange(ExpensesInfosList);
                return ValueList;
            }

            return ValueList;
        }

        public static void SynchronizeWithDB()
        {

        } 

        private static void Pull(Type type)
        {
            if (File.Exists(string.Format(@"MoneyElements(" + type.Name + ").json")))
            {
                using (StreamReader file = File.OpenText(string.Format(@"MoneyElements(" + type.Name + ").json")))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    ExpensesInfosList = (List<ExpensesInfo>)serializer.Deserialize(file, typeof(List<ExpensesInfo>));
                }
            }
            else
            {
                 ExpensesInfosList = new List<ExpensesInfo>();
            }
        }

        private static void Push(Type type)
        {
            if (type == typeof(ExpensesInfo))
            {
                using (StreamWriter file = File.CreateText(string.Format(@"MoneyElements(" + type.Name + ").json")))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, ExpensesInfosList);
                }
            }
        }

    }
}
