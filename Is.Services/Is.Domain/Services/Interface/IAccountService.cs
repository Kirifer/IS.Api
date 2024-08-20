using Is.Core.Abstraction;
using Is.Core.Filtering;
using Is.Shared.Models.Authentication;

namespace Is.Domain.Services.Interface
{
    public interface IAccountService : IEntityService
    {
        Task<Response<AuthUserIdentityDto>> GetIdentityAsync();

        Task<Response<AuthLoginDto>> LoginAsync(AuthLoginRequestDto loginRequest);

        Task<Response<AuthIdentityResultDto>> LogoutAsync();
    }
}
