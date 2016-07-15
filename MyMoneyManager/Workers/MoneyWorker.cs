using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Workers
{
    public enum ExchangeRateEnum
    {
        RU,
        USD,
        EUR
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
    }
}
