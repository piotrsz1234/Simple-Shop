using Data.EF.Contexts;
using Data.EF.Entities;
using Data.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Data.Infrastructure.Repositories
{
    public class UserRepository: RepositoryGenericBase<User>, IUserRepository
    {
        public UserRepository(ShopContext dbContext) : base(dbContext)
        {
        }
    }
}