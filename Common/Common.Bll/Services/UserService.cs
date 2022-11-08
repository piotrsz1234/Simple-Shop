using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Common.Bll.Services.Enums;
using Common.Bll.Services.Interfaces;
using Data.Dto.Dtos;
using Data.Dto.Models;
using Data.EF.Entities;
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

        public IReadOnlyCollection<UserDto>? Search(string name)
        {
            try
            {
                var users = _userRepository.Search(name);

                return Mapper.Map<UserDto[]>(users);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public AddEditUserResult AddEditUser(AddEditUserModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.FirstName) || string.IsNullOrWhiteSpace(model.LastName)
                                                               || string.IsNullOrWhiteSpace(model.LoginCode))
                    return AddEditUserResult.NameFieldsEmpty;

                if (_userRepository.Any(x =>
                    x.IsDeleted == false && x.LoginCode == model.LoginCode && x.Id != model.Id))
                {
                    return AddEditUserResult.CodeNameIsAlreadyUsed;
                }

                User? user;

                if (model.Id.HasValue)
                {
                    user = _userRepository.GetOne(model.Id.Value);
                    if (user is null)
                        return AddEditUserResult.Error;
                    
                    Mapper.Map(model, user);
                }
                else user = Mapper.Map<User>(model);
                
                _userRepository.Add(user);

                user.ModificationDateUTC = DateTime.UtcNow;

                _userRepository.SaveChanges();

                return AddEditUserResult.Ok;
            }
            catch (Exception e)
            {
                return AddEditUserResult.Error;
            }
        }

        public bool RemoveUser(long userId)
        {
            try
            {
                var user = _userRepository.GetOne(userId);

                if (user is null) return false;

                user.IsDeleted = true;
                user.ModificationDateUTC = DateTime.UtcNow;

                _userRepository.SaveChanges();
                
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}