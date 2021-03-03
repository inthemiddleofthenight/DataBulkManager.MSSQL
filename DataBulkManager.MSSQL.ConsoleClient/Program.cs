using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataBulkManager.MSSQL.ConsoleClient
{
    static class Program
    {
        static void Main()
        {
            using (SqlConnection sqlConnection = new SqlConnection(@"Data Source=ELIJAH-PC\SQLEXPRESS;Initial Catalog=BulkTest;Integrated Security=true"))
            {
                Bulk bulk = new Bulk(sqlConnection);
                bulk.Insert<Entity>(GetMockEntities(50000));
            }
        }

        private static IEnumerable<Entity> GetMockEntities(int count)
        {

            List<Entity> entity = new List<Entity>();
            for (int i = 0; i < count; i++)
            {
                entity.Add(new Entity()
                {
                    ID = i + 1,
                    ShortName = $"name {i}",
                    Count = i * 4,
                    Date = DateTime.Now.AddDays(i),
                    Time = DateTime.UtcNow.TimeOfDay
                });
            }

            return entity;
        }
    }
}
