using Macaner.Ecomerce.Application.DTO;
using Macaner.Ecomerce.Transversal.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Macaner.Ecomerce.Application.Interface
{
    public interface ICustomerApplication
    {
        #region Metodos Sincronos
        Response<bool> Insert(CustomerDTO customerDTO);
        Response<bool> Update(CustomerDTO customerDTO);
        Response<bool> Delete(string customerId);
        Response<CustomerDTO> Get(string customerId);
        Response<IEnumerable<CustomerDTO>> GetAll();
        #endregion

        #region Metodos Asincronos
        Task<Response<bool>> InsertAsync(CustomerDTO customerDTO);
        Task<Response<bool>> UpdateAsync(CustomerDTO customerDTO);
        Task<Response<bool>> DeleteAsync(string customerId);
        Task<Response<CustomerDTO>> GetAsync(string customerId);
        Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync();
        #endregion
    }
}
