using System.Net;

using Is.Core.Filtering;
using Is.Domain.Services.Interface;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Is.Api.Controllers
{
    [Authorize()]
    [ApiController]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.InternalServerError)]
    public class IdentityController(IAccountService accountService) : ControllerBase
    {
        private readonly IAccountService accountService = accountService;

        /// <summary>
        /// Gets the identity of the currently logged in user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("identity")]
        [Authorize]
        public async Task<IActionResult> GetIdentityAsync()
        {
            var response = await accountService.GetIdentityAsync();
            return StatusCode((int)response.Code, response);
        }
    }
}