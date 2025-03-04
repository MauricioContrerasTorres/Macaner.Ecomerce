using Macaner.Ecomerce.Domain.Entity;
using Macaner.Ecomerce.Infrastructure.Interface;
using Macaner.Ecomerce.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaner.Ecomerce.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public UserRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Users Authenticate(string login, string password)
        {
            //throw new NotImplementedException();
            List<Users> usersList = new List<Users>();
            usersList.Add(new Users { IdUser = 1, Login = "daguett", Password = "catita", Nombre = "Daguett Castor", Email = "daguett@gmail.com", Departamento = "Esparcimiento" });
            usersList.Add(new Users { IdUser = 1, Login = "norbert", Password = "catita", Nombre = "Norbert Castor", Email = "norbert@gmail.com", Departamento = "Calidad" });
            usersList.Add(new Users { IdUser = 1, Login = "munon", Password = "catita", Nombre = "Muñon del Arbol", Email = "munon@gmail.com", Departamento = "Servicios Generales" });

            Users userSearch = usersList.Where(t=> t.Login == login && t.Password == password).FirstOrDefault();

            return userSearch;
        }
    }
}
