using Macaner.Ecomerce.Application.DTO;
using Macaner.Ecomerce.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaner.Ecomerce.Application.Interface
{
    public interface IUsersApplication
    {
        Response<UsersDTO> Authenticate (string login, string password);
    }
}
