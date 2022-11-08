using System.Collections.Generic;
using Data.EF.Entities;

namespace Data.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryGenericBase<User>
    {
        IReadOnlyCollection<User> Search(string name);
    }
}