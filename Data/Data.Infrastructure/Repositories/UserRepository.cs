using System;
using System.Collections.Generic;
using System.Linq;
using Data.EF.Contexts;
using Data.EF.Entities;
using Data.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Infrastructure.Repositories
{
    public class UserRepository: RepositoryGenericBase<User>, IUserRepository
    {
        public UserRepository(ShopContext dbContext) : base(dbContext)
        {
        }

        public IReadOnlyCollection<User> Search(string name)
        {
            try
            {
                return DbContext.User.Where(x => (x.FirstName + " " + x.LastName).Contains(name) && x.IsDeleted == false).AsNoTracking()
                    .ToArray();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}