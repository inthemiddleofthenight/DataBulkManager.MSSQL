using DataBulkManager.MSSQL.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DataBulkManager.MSSQL
{
    public class Bulk
    {
        private readonly SqlConnection _sqlConnection;
        public Bulk(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection
                ?? throw new ArgumentNullException($"{nameof(sqlConnection)} can't be null");
        }

        public void Insert<T>(IEnumerable<T> entities, 
            SqlBulkCopyOptions sqlBulkCopyOptions = SqlBulkCopyOptions.Default, 
            int timeout = 30,
            bool enableStreaming = false,
            int batchSize = 0,
            SqlTransaction sqlTransaction = null) where T : class
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(_sqlConnection, sqlBulkCopyOptions, sqlTransaction)
            {
                DestinationTableName = typeof(T).GetBulkTableName(),
                BulkCopyTimeout = timeout,
                EnableStreaming = enableStreaming,
                BatchSize = batchSize
            };

            DataTable dataTable = new DataTable();

            var attributes = typeof(T).GetBulkColumnAttributes();

            foreach (BulkColumnAttribute bulkColumnAttribute in attributes)
            {
                dataTable.Columns.Add(new DataColumn(bulkColumnAttribute.Name, bulkColumnAttribute.Type));
                sqlBulkCopy.ColumnMappings.Add(bulkColumnAttribute.Name, bulkColumnAttribute.Name);
            }

            foreach (T entity in entities)
            {
                dataTable.Rows.Add(entity.GetBulkEntityValues());
            }

            stopwatch.Stop();

            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            sqlBulkCopy.WriteToServer(dataTable);
        }
    }
}
