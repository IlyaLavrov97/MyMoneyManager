using MyMoneyManager.Model.Expenses.ViewObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Workers
{
    public enum ExchangeRateEnum
    {
        RU = 1,
        USD = 2,
        EUR = 3
    }

    public class MoneyWorker
    {
        static public double GetExchangeRateValue(ExchangeRateEnum ER)
        {
            switch (ER)
            {
                case ExchangeRateEnum.RU:
                    return 1;
                case ExchangeRateEnum.USD:
                    return 66.68;
                case ExchangeRateEnum.EUR:
                    return 77.77;
                default:
                    break;
            }
            return 0;
        }

        static public double GetAverageIncomingValue()
        {
            return 0;
        }

        static public void SetNewExchangeRateInColl(IEnumerable<ViewExpensesInfo> coll, string oldExchangeRate, string newExchangeRate, int decimals)
        {
            foreach (ViewExpensesInfo item in coll)
            {
                item.Expenditure = ChangeCurrency(item.Expenditure, oldExchangeRate, newExchangeRate, decimals);
            }
        }

        static public double ChangeCurrency(double value, string oldExchangeRate, string newExchangeRate, int decimals)
        {
            return value = Math.Round((value * GetExchangeRateValue((ExchangeRateEnum)Enum.Parse(typeof(ExchangeRateEnum), oldExchangeRate)))
                    / GetExchangeRateValue((ExchangeRateEnum)Enum.Parse(typeof(ExchangeRateEnum), newExchangeRate)), decimals);
        }
    }
}
