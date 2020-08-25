using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace AgroPlan.Property.AgroPlan.Property.Api.Infrastructure{
    public abstract class BaseRepository : IDisposable
    {
        protected readonly IDbConnection _connection;
        private Boolean disposed = false;

        public BaseRepository()
        {
            _connection = new SqlConnection("connectionsting");
        }

        public BaseRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        protected void Dispose(bool disposing)
        {
            if(disposed)
            {
                return;
            }

            if(disposing)
            {

            }

            this._connection.Close();
            this._connection.Dispose();

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseRepository()
            => Dispose(false);
    }
}