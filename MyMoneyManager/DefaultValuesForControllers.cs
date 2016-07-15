using System;
using MyMoneyManager.Workers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager
{
    public class DefaultValuesForControllers
    {
        private static DefaultValuesForControllers instance;

        private DateTime defaultDateTime;

        public DateTime DefaultDateTime
        {
            get
            {
                return defaultDateTime;
            }
            private set
            {
                defaultDateTime = value;
            }
        }

        private int defaultDecimals;

        public int DefaultDecimals
        {
            get
            {
                return defaultDecimals;
            }
            set
            {
                defaultDecimals = value;
            }
        }


        private string defaultSearchStringContent;

        public string DefaultSearchStringContent
        {
            get
            {
                return defaultSearchStringContent;
            }
            private set
            {
                defaultSearchStringContent = value;
            }
        }

        private string defaultExchangeRate;

        public string DefaultExchangeRate
        {
            get
            {
                return defaultExchangeRate;
            }
            private set
            {
                defaultExchangeRate = value;
            }
        }

        public static DefaultValuesForControllers Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DefaultValuesForControllers();
                }
                return instance;
            }
        }


        private DefaultValuesForControllers()
        {
            DefaultDateTime = DateTime.Now;
            DefaultSearchStringContent = "Поиск...";
            DefaultExchangeRate = ExchangeRateEnum.RU.ToString();
            DefaultDecimals = 1;
        }

    }
}
