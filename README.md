# DataBulkManager.MSSQL

Fast bulk insert data into MSSQL database.

**Example**

Create mapping data class

```
/* without attribute target table name resolved as 'dbo.{nameof(Class)}'  */
 [BulkTable("test.TestEntity")] 
 public class Entity
 {
 		/*ignore property without [BulkColumn] attribute - default name is nameof(Property)*/
        [BulkColumn]
        public int Count { get; set; }
        
        [BulkColumn]
        public long LongCount { get; set; }
        
        public int ID { get; set; }
        
        /*set Name in [BulkColumn] attribute for set target column name*/
        [BulkColumn(Name = "Name")]
        public string ShortName { get; set; }
        
        /*set Type in [BulkColumn] attribute for set target column type*/
        [BulkColumn(Type = typeof(DateTime))]
        public DateTime Date { get; set; }
        
        [BulkColumn(Type = typeof(TimeSpan))]
        public TimeSpan Time { get; set; }
        
        [BulkColumn(Type = typeof(Guid))]
        public Guid Guid { get; set; }
}
```

and insert data 

```
/*load data - example*/
IEnumerable<Entity> entities = Array.Empty<Entity>();

using (SqlConnection sqlConnection = new SqlConnection(@"{connectionString}"))
{
      Bulk bulk = new Bulk(sqlConnection);
      bulk.Insert<Entity>(entities);
}
```
