using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Workers
{
    public static class EnumWorker
    {
        
        public static object[] GetValuesAndDescriptions(Type enumType)
        {
            var values = Enum.GetValues(enumType).Cast<object>();
            var valuesAndDescriptions = from value in values
                                        select new
                                        {
                                            Value = value,
                                            Description = value.GetType()
                                                    .GetMember(value.ToString())[0]
                                                    .GetCustomAttributes(true)
                                                    .OfType<DescriptionAttribute>()
                                                    .First()
                                                    .Description
                                        };
            return valuesAndDescriptions.ToArray();
        }

        public static object[] GetValuesAndDescriptionsWithoutZeroElement(Type enumType)
        {
            var values = Enum.GetValues(enumType).Cast<object>();
            var valuesAndDescriptions = from value in values
                                        where (int)value != 0
                                        select new
                                        {
                                            Value = value,
                                            Description = value.GetType()
                                                    .GetMember(value.ToString())[0]
                                                    .GetCustomAttributes(true)
                                                    .OfType<DescriptionAttribute>()
                                                    .First()
                                                    .Description
                                        };
            return valuesAndDescriptions.ToArray();
        }
    }
}
