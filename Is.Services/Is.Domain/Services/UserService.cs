
using System.Net;

using Is.Core.Abstraction;
using Is.Core.Filtering;
using Is.Datalayer;
using Is.Datalayer.Entities;
using Is.Datalayer.Interface;
using Is.Domain.Services.Interface;
using Is.Models;
using Is.Shared.Enums;

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Is.Domain.Services
{
    public class UserService: EntityService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(
            IMapper mapper,
            ILogger<UserService> logger,

            IUserRepository userRepository)
            : base(mapper, logger)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<List<AtsUserDto>>> GetUsersAsync(AtsUserFilterDto filter)
        {
            try
            {
                var result = await _userRepository.GetAllAsync(u =>
                string.IsNullOrEmpty(filter.Username) || u.Username == filter.Username);

                var userDtos = Mapper.Map<List<AtsUserDto>>(result);

                return Response<List<AtsUserDto>>.Success(userDtos);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while fetching users");
                return Response<List<AtsUserDto>>.Exception(ex);
            }
        }

        public async Task<Response<AtsUserDto>> GetUserAsync(Guid id)
        {
            try
            {
                var result = await _userRepository.GetAsync(id);
                var userDto = Mapper.Map<AtsUserDto>(result);
                return Response<AtsUserDto>.Success(userDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while fetching user with id.");
                return Response<AtsUserDto>.Exception(ex);
            }
        }

        public async Task<Response<AtsUserDto>> CreateUserAsync(AtsUserCreateDto user)
        {
            try
            {
                var createRef = Mapper.Map<User>(user);

                var result = await _userRepository.AddAsync(createRef);

                var userDto = Mapper.Map<AtsUserDto>(result);

                return Response<AtsUserDto>.Success(userDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while creating user.");
                return Response<AtsUserDto>.Exception(ex);
            }
        }

        public async Task<Response<AtsUserDto>> UpdateUserAsync(Guid id, AtsUserUpdateDto user)
        {
            try
            {
                var updateRef = await _userRepository.GetAsync(id);

                updateRef.FirstName = user.FirstName;
                updateRef.LastName = user.LastName;
                updateRef.Password = user.Password;

                var result = await _userRepository.UpdateAsync(updateRef);

                var userDto = Mapper.Map<AtsUserDto>(result);

                return Response<AtsUserDto>.Success(userDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while updating user.");
                return Response<AtsUserDto>.Exception(ex);
            }
        }

        public async Task<Response<AtsUserDto>> DeleteUserAsync(Guid id)
        {
            try
            {
                var deleteRef = await _userRepository.GetAsync(id);

                var result = await _userRepository.DeleteAsync(deleteRef);

                var userDto = Mapper.Map<AtsUserDto>(result);

                return Response<AtsUserDto>.Success(userDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while deleting user.");
                return Response<AtsUserDto>.Exception(ex);
            }
        }
    }
}
