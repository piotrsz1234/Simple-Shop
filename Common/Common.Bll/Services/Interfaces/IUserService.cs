using Data.Dto.Dtos;

namespace Common.Bll.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> LoginUserAsync(string code);
    }
}