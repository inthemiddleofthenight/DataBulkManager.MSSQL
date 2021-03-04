using DataBulkManager.MSSQL.Attributes;
using System;

namespace DataBulkManager.MSSQL.ConsoleClient
{

    [BulkTable("test.TestEntity")]
    public class Entity
    {
        [BulkColumn]
        public int Count { get; set; }
        [BulkColumn]
        public long LongCount { get; set; }
        public int ID { get; set; }
        [BulkColumn(Name = "Name")]
        public string ShortName { get; set; }
        [BulkColumn(Type = typeof(DateTime))]
        public DateTime Date { get; set; }
        [BulkColumn(Type = typeof(TimeSpan))]
        public TimeSpan Time { get; set; }
        [BulkColumn(Type = typeof(Guid))]
        public Guid Guid { get; set; }
    }
}
