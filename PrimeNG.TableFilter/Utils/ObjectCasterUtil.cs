using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Linq;
using PrimeNG.TableFilter.Models;

namespace PrimeNG.TableFilter.Utils
{
    public static class ObjectCasterUtil
    {
        public static object CastPropertiesTypeList(PropertyInfo property, object value)
        {
            var arrayCast = (JArray)value;
            if (property?.PropertyType == typeof(int))
                return arrayCast.ToObject<List<int>>();
            if (property?.PropertyType == typeof(int?))
                return arrayCast.ToObject<List<int?>>();
            if (property?.PropertyType == typeof(double))
                return arrayCast.ToObject<List<double>>();
            if (property?.PropertyType == typeof(double?))
                return arrayCast.ToObject<List<double?>>();
            if (property?.PropertyType == typeof(DateTime))
                return arrayCast.ToObject<List<DateTime>>();
            if (property?.PropertyType == typeof(DateTime?))
                return arrayCast.ToObject<List<DateTime?>>();
            if (property?.PropertyType == typeof(DateTimeOffset))
                return arrayCast.ToObject<List<DateTimeOffset>>();
            if (property?.PropertyType == typeof(DateTimeOffset?))
                return arrayCast.ToObject<List<DateTimeOffset?>>();
            if (property?.PropertyType == typeof(bool))
                return arrayCast.ToObject<List<bool>>();
            if (property?.PropertyType == typeof(bool?))
                return arrayCast.ToObject<List<bool?>>();
            if (property?.PropertyType == typeof(short))
                return arrayCast.ToObject<List<short>>();
            if (property?.PropertyType == typeof(short?))
                return arrayCast.ToObject<List<short?>>();
            if (property?.PropertyType == typeof(long))
                return arrayCast.ToObject<List<long>>();
            if (property?.PropertyType == typeof(long?))
                return arrayCast.ToObject<List<long?>>();
            if (property?.PropertyType == typeof(float))
                return arrayCast.ToObject<List<float>>();
            if (property?.PropertyType == typeof(float?))
                return arrayCast.ToObject<List<float?>>();
            if (property?.PropertyType == typeof(decimal))
                return arrayCast.ToObject<List<decimal>>();
            if (property?.PropertyType == typeof(decimal?))
                return arrayCast.ToObject<List<decimal?>>();
            if (property?.PropertyType == typeof(byte))
                return arrayCast.ToObject<List<byte>>();
            if (property?.PropertyType == typeof(byte?))
                return arrayCast.ToObject<List<byte?>>();
            if (property?.PropertyType.IsEnum == true)
            {
                Type listType = typeof(List<>).MakeGenericType(property?.PropertyType);
                var enumArray = (IList)Activator.CreateInstance(listType);
                // Cast each element of the input array to the enum type and add to the enum array
                foreach (var row in arrayCast)
                {
                    enumArray.Add(Enum.ToObject(property?.PropertyType,Convert.ToByte(row)));
                }
                return enumArray;
            }
            if (property?.PropertyType.IsGenericType == true &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                property.PropertyType.GetGenericArguments()[0].IsEnum)
            {
                Type enumType = property.PropertyType.GetGenericArguments()[0];

                // Create a nullable enum type
                Type nullableEnumType = typeof(Nullable<>).MakeGenericType(enumType);

                // Create a list of nullable enum type
                Type listType = typeof(List<>).MakeGenericType(nullableEnumType);
                var enumNullableList = (IList)Activator.CreateInstance(listType);

                // Cast each element of the input array to the nullable enum type and add to the nullable list
                foreach (var row in arrayCast)
                {
                    var enumValue = Enum.ToObject(enumType, Convert.ToByte(row));
                    var nullableEnumValue = Activator.CreateInstance(nullableEnumType, enumValue);
                    enumNullableList.Add(nullableEnumValue);
                }

                return enumNullableList;
            }

            return arrayCast.ToObject<List<string>>();
        }

        public static object CastPropertiesType(PropertyInfo property, object value)
        {

            if (property?.PropertyType == typeof(int))
                return Convert.ToInt32(value);
            if (property?.PropertyType == typeof(int?))
                return Convert.ToInt32(value);
            if (property?.PropertyType == typeof(double))
                return Convert.ToDouble(value);
            if (property?.PropertyType == typeof(double?))
                return Convert.ToDouble(value);
            if (property?.PropertyType == typeof(DateTime))
                return Convert.ToDateTime(value);
            if (property?.PropertyType == typeof(DateTime?))
                return Convert.ToDateTime(value);
            if (property?.PropertyType == typeof(DateTimeOffset))
                return DateTimeOffset.Parse(value.ToString());
            if (property?.PropertyType == typeof(DateTimeOffset?))
                return DateTimeOffset.Parse(value.ToString());
            if (property?.PropertyType == typeof(bool))
                return Convert.ToBoolean(value);
            if (property?.PropertyType == typeof(bool?))
                return Convert.ToBoolean(value);
            if (property?.PropertyType == typeof(short))
                return Convert.ToInt16(value);
            if (property?.PropertyType == typeof(short?))
                return Convert.ToInt16(value);
            if (property?.PropertyType == typeof(long))
                return Convert.ToInt64(value);
            if (property?.PropertyType == typeof(long?))
                return Convert.ToInt64(value);
            if (property?.PropertyType == typeof(float))
                return Convert.ToSingle(value);
            if (property?.PropertyType == typeof(float?))
                return Convert.ToSingle(value);
            if (property?.PropertyType == typeof(decimal))
                return Convert.ToDecimal(value);
            if (property?.PropertyType == typeof(decimal?))
                return Convert.ToDecimal(value);
            if (property?.PropertyType == typeof(byte))
                return Convert.ToByte(value);
            if (property?.PropertyType == typeof(byte?))
                return Convert.ToByte(value);
            if (property?.PropertyType.IsEnum == true)
                return Enum.ToObject(property.PropertyType, value);
            if (property?.PropertyType.IsGenericType == true &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                property.PropertyType.GetGenericArguments()[0].IsEnum)
            {
                Type enumType = property.PropertyType.GetGenericArguments()[0];
                return Enum.ToObject(enumType, value);
            }

            return value.ToString();
        }

        public static TableFilterContext CastJObjectToTableFilterContext(JObject obj)
            => obj.ToObject<TableFilterContext>();
    }
}