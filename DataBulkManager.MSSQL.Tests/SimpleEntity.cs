using DataBulkManager.MSSQL.Attributes;
using System;

namespace DataBulkManager.MSSQL.Tests
{
    class SimpleEntity
    {
        [BulkColumn(Name = "Name", Type = typeof(DateTime))]
        public string Name { get; set; }

        public int Count { get; set; }
    }
}
