using Macaner.Ecomerce.Domain.Entity;
using Macaner.Ecomerce.Infrastructure.Interface;
using Macaner.Ecomerce.Transversal.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace Macaner.Ecomerce.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public CustomerRepository(IConnectionFactory connectionFactory)
        {   
            
            _connectionFactory = connectionFactory;
        }

        #region Metodos Sincronos

        public bool Delete(string customerId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "EXEC InsertCostumer @CustomerID";

                var result = connection.Execute(sql, new
                {
                    CustomerID = customerId,                    
                }, commandType: System.Data.CommandType.StoredProcedure);

                return result > 0;
            }
        }      

        public Customer Get(string customerId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "EXEC GetCustomers @CustomerID";

                var result = connection.QuerySingle<Customer>(sql, new
                {
                    CustomerID = customerId                    
                });

                return result;
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "EXEC GetCustomers";

                var result = connection.Query<Customer>(sql);

                return result;
            }
        }

        

        public bool Insert(Customer customer)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "EXEC InsertCostumer @CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax";

                var result = connection.Execute(sql, new
                {
                    CustomerID = customer.CustomerId,
                    CompanyName = customer.CompanyName,
                    ContactName = customer.ContactName,
                    ContactTitle = customer.ContactTitle,
                    Address = customer.Address,
                    City = customer.City,
                    Region = customer.Region,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country,
                    Phone = customer.Phone,
                    Fax = customer.Fax
                });

                return result > 0;
            }

           
        }        

        public bool Update(Customer customer)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "EXEC UpdateCostumer @CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax";

                var result = connection.Execute(sql, new
                {
                    CustomerID = customer.CustomerId,
                    CompanyName = customer.CompanyName,
                    ContactName = customer.ContactName,
                    ContactTitle = customer.ContactTitle,
                    Address = customer.Address,
                    City = customer.City,
                    Region = customer.Region,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country,
                    Phone = customer.Phone,
                    Fax = customer.Fax
                });

                return result > 0;
            }
        }
        #endregion

        #region Metodos Asincronos
        public async Task<bool> DeleteAsync(string customerId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "EXEC InsertCostumer @CustomerID";

                var result = await connection.ExecuteAsync(sql, new
                {
                    CustomerID = customerId,
                });

                return result > 0;
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "EXEC GetCustomers";

                var result = await connection.QueryAsync<Customer>(sql);

                return result;
            }
        }



        public async Task<Customer> GetAsync(string customerId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "EXEC GetCustomers @CustomerID";

                var result = await connection.QuerySingleAsync<Customer>(sql, new
                {
                    CustomerID = customerId
                });

                return result;
            }
        }

        public async Task<bool> InsertAsync(Customer customer)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "EXEC InsertCostumer @CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax";

                var result = await connection.ExecuteAsync(sql, new
                {
                    CustomerID = customer.CustomerId,
                    CompanyName = customer.CompanyName,
                    ContactName = customer.ContactName,
                    ContactTitle = customer.ContactTitle,
                    Address = customer.Address,
                    City = customer.City,
                    Region = customer.Region,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country,
                    Phone = customer.Phone,
                    Fax = customer.Fax
                }, commandType: System.Data.CommandType.StoredProcedure);

                return result > 0;
            }
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "EXEC UpdateCostumer @CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax";

                var result = await connection.ExecuteAsync(sql, new
                {
                    CustomerID = customer.CustomerId,
                    CompanyName = customer.CompanyName,
                    ContactName = customer.ContactName,
                    ContactTitle = customer.ContactTitle,
                    Address = customer.Address,
                    City = customer.City,
                    Region = customer.Region,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country,
                    Phone = customer.Phone,
                    Fax = customer.Fax
                });

                return result > 0;
            }

            
        }
        #endregion
    }
}
