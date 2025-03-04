using Macaner.Ecomerce.Domain.Interface;
using Macaner.Ecomerce.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Macaner.Ecomerce.Transversal.Common;
using Macaner.Ecomerce.Application.DTO;

namespace Macaner.Ecomerce.Application.Main
{
    public class UsersApplication : IUsersApplication
    {
        private readonly IUserDomain _userDomain;
        private readonly IMapper _mapper;

        public UsersApplication(IUserDomain userDomain, IMapper mapper)
        {
            _userDomain = userDomain;
            _mapper = mapper;
        }

        public Response<UsersDTO> Authenticate(string login, string password)
        {
            var response = new Response<UsersDTO>();

            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
            {
                response.Message = "login y pass no pueden ir vacios";
                return response;
            }

            try
            {
                var user = _userDomain.Authenticate(login, password);
                response.Data = _mapper.Map<UsersDTO>(user);
                response.IsSuccess = true;
                response.Message = "Autenticación pulenta";
            }
            catch (InvalidCastException)
            {
                response.IsSuccess = true;
                response.Message = "Usuario no existe";
            }
            catch (Exception ex)
            {
                response.Message += ex.Message;             
            }

            return response;
        }
    }
}
