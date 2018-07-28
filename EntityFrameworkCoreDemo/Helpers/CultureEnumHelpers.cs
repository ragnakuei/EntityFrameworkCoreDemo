using System;
using System.ComponentModel;
using EntityFrameworkCoreDemo.Enums;

namespace EntityFrameworkCoreDemo.Helpers
{
    public static class CultureEnumHelpers
    {
        public static int ToIntValue(this CultureEnum e)
        {
            return (int) e;
        }

        public static string GetDescription(this CultureEnum e)
        {
            if (!e.GetType().IsEnum)
            {
                throw new ArgumentException("Input type must be an enumerated type");
            }

            var data      = e.GetType().GetField(e.ToString());
            var attribute = (DescriptionAttribute) Attribute.GetCustomAttribute(data, typeof(DescriptionAttribute));
            return attribute.Description;
        }
    }
}