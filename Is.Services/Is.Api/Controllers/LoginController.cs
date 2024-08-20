using System.Net;

using Is.Core.Config;
using Is.Core.Extensions;
using Is.Core.Filtering;
using Is.Domain.Services.Interface;
using Is.Shared.Extensions;
using Is.Shared.Models.Authentication;

using Microsoft.AspNetCore.Mvc;

namespace Is.Api.Controllers
{
    [ApiController]
    public class LoginController(IMicroServiceConfig serviceConfig,
        IAccountService accountService) : ControllerBase
    {
        private readonly IAccountService accountService = accountService;

        /// <summary>
        /// Login the user based on credentials
        /// </summary>
        /// <param name="request">The request details of the user to be logged in</param>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(Response<AuthLoginDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoginAsync([FromBody] AuthLoginRequestDto request)
        {
            var uriHost = new Uri(Request.GetAbsoluteUrl());
            var response = await accountService.LoginAsync(request);

            // Save the token to cookie as HTTP Only Mode
            if (response.Succeeded && !Equals(response.Data?.Token, null))
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1),
                    HttpOnly = true,
                    Domain = uriHost.GetDomainName(),
                    Secure = true,
                    SameSite = SameSiteMode.None // Disable for localhost
                };

                Response.Cookies.Append(serviceConfig.JwtConfig!.CookieName, response.Data?.Token!, cookieOptions);
            }

            return StatusCode((int)response.Code, response);
        }
    }
}