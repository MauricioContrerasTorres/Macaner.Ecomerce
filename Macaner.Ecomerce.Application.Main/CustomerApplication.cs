using AutoMapper;
using Macaner.Ecomerce.Application.DTO;
using Macaner.Ecomerce.Application.Interface;
using Macaner.Ecomerce.Domain.Entity;
using Macaner.Ecomerce.Domain.Interface;
using Macaner.Ecomerce.Transversal.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Macaner.Ecomerce.Application.Main
{
    public class CustomerApplication : ICustomerApplication
    {
        private readonly ICustomerDomain _customerDomain;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CustomerApplication> _logger;

        public CustomerApplication(ICustomerDomain customerDomain, IMapper mapper, IAppLogger<CustomerApplication> logger)
        {
            _customerDomain = customerDomain;
            _mapper = mapper;
            _logger = logger;
        }

        #region Metodos Sincronos
        public Response<bool> Insert(CustomerDTO customerDTO)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customer>(customerDTO);
                response.Data = _customerDomain.Insert(customer);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;                
            }

            return response;
        }
        public Response<bool> Update(CustomerDTO customerDTO)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customer>(customerDTO);
                response.Data = _customerDomain.Update(customer);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualizacion Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        public Response<bool> Delete(string customerId)
        {
            var response = new Response<bool>();
            try
            {                
                response.Data = _customerDomain.Delete(customerId);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminación Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }
        public Response<CustomerDTO> Get(string customerId)
        {
            var response = new Response<CustomerDTO>();
            try
            {
                var customer = _customerDomain.Get(customerId);
                response.Data = _mapper.Map<CustomerDTO>(customer);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }
        public Response<IEnumerable<CustomerDTO>> GetAll(){
            var response = new Response<IEnumerable<CustomerDTO>>();
            try
            {
                var customers = _customerDomain.GetAll();
                response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customers);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                    _logger.LogInformation(response.Message);
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message+e.Source+e.InnerException+e.StackTrace;
                _logger.LogError(response.Message);
            }

            return response;
        }
        #endregion

        #region Metodos Asincronos
        public async Task<Response<bool>> InsertAsync(CustomerDTO customerDTO)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customer>(customerDTO);
                response.Data = await _customerDomain.InsertAsync(customer);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }
        public async Task<Response<bool>> UpdateAsync(CustomerDTO customerDTO)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customer>(customerDTO);
                response.Data = await _customerDomain.UpdateAsync(customer);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualizacion Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }
        public async Task<Response<bool>> DeleteAsync(string customerId)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = await _customerDomain.DeleteAsync(customerId);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminación Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }
        public async Task<Response<CustomerDTO>> GetAsync(string customerId)
        {
            
            var response = new Response<CustomerDTO>();
            try
            {
                var customer = await _customerDomain.GetAsync(customerId);
                response.Data = _mapper.Map<CustomerDTO>(customer);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
            
        }
        public async Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<CustomerDTO>>();
            try
            {
                var customers = await _customerDomain.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customers);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }
        #endregion
    }
}
