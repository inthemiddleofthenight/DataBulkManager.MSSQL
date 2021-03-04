using System;

namespace DataBulkManager.MSSQL.Exceptions
{
    public class DbmException : Exception
    {
        public DbmException(string message) : base(message)
        {

        }

        public DbmException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
