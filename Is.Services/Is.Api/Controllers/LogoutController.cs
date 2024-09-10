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
    public class LogoutController(
        IMicroServiceConfig config,
        IAccountService accountService) : ControllerBase
    {
        private readonly IMicroServiceConfig config = config;
        private readonly IAccountService accountService = accountService;

        /// <summary>
        /// Logout the logged in user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("logout")]
        [ProducesResponseType(typeof(Response<AuthIdentityResultDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LogoutAsync()
        {
            var response = await accountService.LogoutAsync();
            var uriHost = new Uri(Request.GetAbsoluteUrl());
            var cookieOptions = new CookieOptions
            {
                Domain = uriHost.GetDomainName(),

                 HttpOnly = true,
                 Secure = true,
                 SameSite = SameSiteMode.None // Disable for localhost
            };

            Response.Cookies.Delete(config.JwtConfig!.CookieName, cookieOptions);
            return StatusCode((int)response.Code, response);
        }
    }
}