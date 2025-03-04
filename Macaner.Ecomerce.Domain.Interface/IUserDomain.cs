using Macaner.Ecomerce.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaner.Ecomerce.Domain.Interface
{
    public interface IUserDomain
    {
        Users Authenticate (string login, string password);
    }
}
