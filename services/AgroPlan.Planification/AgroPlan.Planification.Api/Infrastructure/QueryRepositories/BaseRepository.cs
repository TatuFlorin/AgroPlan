using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Npgsql;

namespace AgroPlan.Planification.Api.Infrastructure.QueryRepositories
{
    public class BaseRepository : IDisposable
    {
        protected readonly IDbConnection _conn;
        private bool isDisposed = false;

        protected BaseRepository(string connString)
        {
            _conn = new NpgsqlConnection(connString);
        }

        //Dispose pattern
        protected void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            if (disposing)
            {
                _conn.Close();
                _conn.Dispose();
            }

            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseRepository() { Dispose(false); }
    }
}