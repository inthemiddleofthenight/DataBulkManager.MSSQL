using System;

namespace DataBulkManager.MSSQL.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class BulkTableAttribute : Attribute
    {
        public readonly string Name;
        public BulkTableAttribute(string name)
        {
            Name = name;
        }
    }
}
