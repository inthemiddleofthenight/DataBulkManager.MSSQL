using DataBulkManager.MSSQL.Attributes;
using DataBulkManager.MSSQL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

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
            try
            {
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

                if (_sqlConnection.State != ConnectionState.Open)
                    _sqlConnection.Open();

                sqlBulkCopy.WriteToServer(dataTable);
            }
            catch (Exception ex)
            {
                throw new DbmException($"{nameof(DataBulkManager.MSSQL.Bulk)} {nameof(Insert)}<{typeof(T).Name}> error", ex);
            }
        }

        public async Task InsertAsync<T>(IEnumerable<T> entities,
          SqlBulkCopyOptions sqlBulkCopyOptions = SqlBulkCopyOptions.Default,
          int timeout = 30,
          bool enableStreaming = false,
          int batchSize = 0,
          SqlTransaction sqlTransaction = null) where T : class
        {
            try
            {
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

                if (_sqlConnection.State != ConnectionState.Open)
                    _sqlConnection.Open();

                await sqlBulkCopy.WriteToServerAsync(dataTable);
            }
            catch (Exception ex)
            {
                throw new DbmException($"{nameof(DataBulkManager.MSSQL.Bulk)} {nameof(InsertAsync)}<{typeof(T).Name}> error", ex);
            }
        }
    }
}
