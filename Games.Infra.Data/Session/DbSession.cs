using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Games.Infra.Data.Session
{
    public class DbSession : IDisposable
    {
        private readonly IConfiguration _configuration;

        public DbSession(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private Guid _id;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession()
        {
            _id = Guid.NewGuid();
            Connection = new SqlConnection(_configuration.GetConnectionString("GameApiConnectionString"));
            Connection.Open();
        }
        public void Dispose() => Connection?.Dispose();
    }
}