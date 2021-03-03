using System;

namespace DataBulkManager.MSSQL.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class BulkColumnAttribute : Attribute
    {
        public string Name { get; set; }
        public Type Type { get; set; } = typeof(string);
    }
}
