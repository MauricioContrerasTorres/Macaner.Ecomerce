using Macaner.Ecomerce.Transversal.Common;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;

namespace Macaner.Ecomerce.Infrastructure.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration)
        {
            try
            {
                _configuration = configuration;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                throw;
            }
           
        }

        public IDbConnection GetConnection
        {
            get
            {
                try
                {
                    var sqlConnection = new SqlConnection();
                    if (sqlConnection == null) return null;

                    sqlConnection.ConnectionString = _configuration.GetConnectionString("NorthwindConnection");
                    sqlConnection.Open();

                    return sqlConnection;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
                
            }
        }
    }
}
