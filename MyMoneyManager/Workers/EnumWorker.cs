using MyMoneyManager.Model.Expenses;
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

        public static string GetDescriptionFromValue(string value)
        {
            ExpensesType Value = (ExpensesType)Enum.Parse(typeof(ExpensesType), value);
            string Description = Value.GetType().GetMember(value)[0]
                                                    .GetCustomAttributes(true)
                                                    .OfType<DescriptionAttribute>()
                                                    .First()
                                                    .Description;
            return Description;
        }

        public static ExpensesType GetValueFromDescription(string description)
        {
            var values = Enum.GetValues(typeof(ExpensesType)).Cast<object>();
            foreach (var value in values)
            {
                if (value.GetType().GetMember(value.ToString())[0].GetCustomAttributes(true).OfType<DescriptionAttribute>().First().Description == description)
                {
                    return (ExpensesType)value;
                }
            }
            return ExpensesType.Any;
        }
    }
}
