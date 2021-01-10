using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WalletSystemAPI.Helpers
{
    public static class ReflectionConverter
    {
        public static object GetPropertyValue(object source, string propertyName)
        {
            PropertyInfo property = source.GetType().GetProperty(propertyName);
            return property?.GetValue(source, null);
        }

        public static List<PropertyInfo> GetPropertyValues(object source)
        {
            PropertyInfo[] property = source.GetType().GetProperties();
            return property.ToList();
        }
    }
}