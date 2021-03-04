using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DataBulkManager.MSSQL.ConsoleClient
{
    static class Program
    {
        static void Main()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(@"Data Source=ELIJAH-PC\SQLEXPRESS;Initial Catalog=BulkTest;Integrated Security=true"))
                {
                    Bulk bulk = new Bulk(sqlConnection);
                  
                    var entities1 = GetMockEntities(50000);
                    
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    bulk.Insert<Entity>(entities1);
                    stopwatch.Stop();
                    Console.WriteLine($"50000 {stopwatch.Elapsed}");

                    entities1 = GetMockEntities(100000);

                    stopwatch.Restart();
                    bulk.Insert<Entity>(entities1);
                    stopwatch.Stop();
                    Console.WriteLine($"100000 {stopwatch.Elapsed}");

                    entities1 = GetMockEntities(300000);

                    stopwatch.Restart();
                    bulk.Insert<Entity>(entities1);
                    stopwatch.Stop();
                    Console.WriteLine($"300000 {stopwatch.Elapsed}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
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
                    Time = DateTime.UtcNow.TimeOfDay,
                    Guid = Guid.NewGuid()
                });
            }

            return entity;
        }
    }
}
