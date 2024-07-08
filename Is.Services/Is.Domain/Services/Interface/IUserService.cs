using Is.Core.Abstraction;
using Is.Core.Filtering;
using Is.Models;

namespace Is.Domain.Services.Interface
{
    public interface IUserService : IEntityService
    {
        Task<Response<List<AtsUserDto>>> GetUsersAsync(AtsUserFilterDto filter);

        Task<Response<AtsUserDto>> GetUserAsync(Guid id);

        Task<Response<AtsUserDto>> CreateUserAsync(AtsUserCreateDto user);

        Task<Response<AtsUserDto>> UpdateUserAsync(Guid id, AtsUserUpdateDto user);

        Task<Response<AtsUserDto>> DeleteUserAsync(Guid id);
    }
}
