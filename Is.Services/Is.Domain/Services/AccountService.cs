
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

using AutoMapper;

using Is.Core.Abstraction;
using Is.Core.Authentication;
using Is.Core.Config;
using Is.Core.Filtering;
using Is.Datalayer.Entities;
using Is.Datalayer.Interface;
using Is.Domain.Services.Interface;
using Is.Shared;
using Is.Shared.Enums;
using Is.Shared.Models.Authentication;

using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Is.Domain.Services
{
    public class AccountService : EntityService, IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;
        private readonly IMicroServiceConfig _microServiceConfig;

        public AccountService(
            IMapper mapper,
            ILogger<AccountService> logger,
            IUserContext userContext,
            IMicroServiceConfig microServiceConfig,

            IUserRepository userRepository)
            : base(mapper, logger)
        {
            _userRepository = userRepository;
            _userContext = userContext;
            _microServiceConfig = microServiceConfig;
        }

        public async Task<Response<AuthUserIdentityDto>> GetIdentityAsync()
        {
            try
            {
                // Validate if the user is authenticated and has a valid token
                var user = await _userRepository.GetByEmailAsync(_userContext.Email);
                if (user == null)
                {
                    return Response<AuthUserIdentityDto>.Error(HttpStatusCode.Unauthorized,
                        new ErrorDto(ErrorCode.NoRecordFound, "User not found."));
                }

                var response = new AuthUserIdentityDto
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsAdmin = user.IsAdmin,
                };

                return Response<AuthUserIdentityDto>.Success(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while fetching identity.");
                return Response<AuthUserIdentityDto>.Exception(ex);
            }
        }

        public async Task<Response<AuthLoginDto>> LoginAsync(AuthLoginRequestDto loginRequest)
        {
            try
            {
                var user = await _userRepository.GetByUserNamePasswordAsync(loginRequest.UserName, loginRequest.Password);
                if (user == null)
                {
                    return Response<AuthLoginDto>.Error(HttpStatusCode.Unauthorized,
                        new ErrorDto(ErrorCode.NoRecordFound, "Invalid username or password."));
                }

                var response = new AuthLoginDto
                {
                    Succeeded = true,
                };

                // Generate Token
                var generateToken = GenerateToken(user);
                response.Token = generateToken.ToString();
                return Response<AuthLoginDto>.Success(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while logging in.");
                return Response<AuthLoginDto>.Exception(ex);
            }
        }

        public async Task<Response<AuthIdentityResultDto>> LogoutAsync()
        {
            // In case of using Identity Server, we can revoke the user logged in

            return Response<AuthIdentityResultDto>.Success(new AuthIdentityResultDto { Succeeded = true });
        }

        private object GenerateToken(User user)
        {
            // Generate JwtSecurityToken
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_microServiceConfig.JwtConfig!.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(1); // 1 day

            var generatedToken = new JwtSecurityToken(
                issuer: _microServiceConfig.JwtConfig.Issuer,
                audience: _microServiceConfig.JwtConfig.Audience,
                claims: new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(AuthClaims.FullName, user.FullName),
                    new Claim(AuthClaims.Admin, user.IsAdmin.ToString()),
                    //new Claim(ClaimTypes.Role, user.Role.ToString())
                },
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(generatedToken);
        }
    }
}
