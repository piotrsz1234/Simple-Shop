using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Bll.Services.Enums;
using Data.Dto.Dtos;
using Data.Dto.Models;

namespace Common.Bll.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> LoginUserAsync(string code);
        IReadOnlyCollection<UserDto>? Search(string name);
        AddEditUserResult AddEditUser(AddEditUserModel model);
        bool RemoveUser(long userId);
        UserDto? GetOne(long id);
    }
}