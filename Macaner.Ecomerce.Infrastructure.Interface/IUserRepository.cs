using Macaner.Ecomerce.Domain.Entity;

namespace Macaner.Ecomerce.Infrastructure.Interface
{
    public interface IUserRepository
    {
        Users Authenticate(string username, string password);
    }
}
