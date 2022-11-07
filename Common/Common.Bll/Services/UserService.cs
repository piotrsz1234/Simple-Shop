using AutoMapper;
using Common.Bll.Services.Interfaces;
using Data.Dto.Dtos;
using Data.Infrastructure.Repositories.Interfaces;

namespace Common.Bll.Services
{
    internal class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository _userRepository;
        
        public UserService(IMapper mapper, IUserRepository userRepository) : base(mapper)
        {
            _userRepository = userRepository;
        }
        
        public async Task<UserDto?> LoginUserAsync(string code)
        {
            try {
                var user = await _userRepository.GetOneAsync(x => x.LoginCode == code && x.IsDeleted == false);

                if (user is null) return null;
                
                return Mapper.Map<UserDto>(user);
            }
            catch (Exception e) {
                return null;
            }
        }
    }
}