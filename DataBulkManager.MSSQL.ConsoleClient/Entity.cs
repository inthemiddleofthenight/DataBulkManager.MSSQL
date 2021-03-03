using DataBulkManager.MSSQL.Attributes;
using System;

namespace DataBulkManager.MSSQL.ConsoleClient
{

    [BulkTable("test.TestEntity")]
    public class Entity
    {
        public int ID { get; set; }
        [BulkColumn(Name = "Name")]
        public string ShortName { get; set; }
        [BulkColumn(Type = typeof(DateTime))]
        public DateTime Date { get; set; }
        [BulkColumn(Type = typeof(TimeSpan))]
        public TimeSpan Time { get; set; }
        [BulkColumn]
        public int Count { get; set; }
    }
}
