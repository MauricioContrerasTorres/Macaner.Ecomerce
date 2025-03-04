using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaner.Ecomerce.Domain.Entity;
using Macaner.Ecomerce.Domain.Interface;
using Macaner.Ecomerce.Infrastructure.Interface;

namespace Macaner.Ecomerce.Domain.Core
{
    public class CustomerDomain : ICustomerDomain
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerDomain(ICustomerRepository customerDomain)
        {
            _customerRepository = customerDomain;
        }

        public bool Delete(string customerId)
        {
            return _customerRepository.Delete(customerId);
        }        

        public Customer Get(string customerId)
        {
            return _customerRepository.Get(customerId);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }                

        public bool Insert(Customer customer)
        {
            return _customerRepository.Insert(customer);
        }        

        public bool Update(Customer customer)
        {
            return _customerRepository.Update(customer);
        }

        public async Task<bool> DeleteAsync(string customerId)
        {
            return await _customerRepository.DeleteAsync(customerId);
        }

        public async Task<Customer> GetAsync(string customerId)
        {
            return await _customerRepository.GetAsync(customerId);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<bool> InsertAsync(Customer customer)
        {
            return await _customerRepository.InsertAsync(customer);
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            return await _customerRepository.UpdateAsync(customer);
        }
    }
}
