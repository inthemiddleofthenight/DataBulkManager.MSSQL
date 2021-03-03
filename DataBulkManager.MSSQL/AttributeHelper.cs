using DataBulkManager.MSSQL.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataBulkManager.MSSQL
{
    public static class AttributeHelper
    {
        public static string GetBulkTableName(this Type type)
        {
            BulkTableAttribute bulkTableAttribute =
              (BulkTableAttribute)Attribute.GetCustomAttribute(type, typeof(BulkTableAttribute));

            return bulkTableAttribute?.Name ?? $"dbo.{type.Name}";
        }

        public static IEnumerable<BulkColumnAttribute> GetBulkColumnAttributes(this Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
                .Select(x => new
                {
                    DefaultName = x.Name,
                    Attr = (BulkColumnAttribute)Attribute.GetCustomAttribute(x, typeof(BulkColumnAttribute), false)
                })
                .Where(x => x.Attr != null)
                .Select(x => new BulkColumnAttribute()
                {
                    Name = x.Attr.Name ?? x.DefaultName,
                    Type = x.Attr.Type,
                });
        }

        public static object[] GetBulkEntityValues(this object obj)
        {
            return obj
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
                .Where(x => Attribute.IsDefined(x, typeof(BulkColumnAttribute)))
                .Select( x => x.GetValue(obj))
                .ToArray();
        }
    }
}
