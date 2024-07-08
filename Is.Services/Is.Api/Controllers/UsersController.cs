using System.Net;

using Is.Core.Filtering;
using Is.Domain.Services.Interface;
using Is.Models;

using Microsoft.AspNetCore.Mvc;

namespace Is.Api.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.InternalServerError)]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService userService = userService;

        [HttpGet]
        [Route("users")]
        [ProducesResponseType(typeof(Response<List<AtsUserDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsersAsync([FromQuery] AtsUserFilterDto filter)
        {
            var response = await userService.GetUsersAsync(filter);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet]
        [Route("users/{id}")]
        [ProducesResponseType(typeof(Response<AtsUserDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserAsync(Guid id)
        {
            var response = await userService.GetUserAsync(id);
            return StatusCode((int)response.Code, response);
        }

        [HttpPost]
        [Route("users")]
        [ProducesResponseType(typeof(Response<AtsUserDto>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddUserAsync([FromBody] AtsUserCreateDto user)
        {
            var response = await userService.CreateUserAsync(user);
            return StatusCode((int)response.Code, response);
        }

        [HttpPut]
        [Route("users/{id}")]
        [ProducesResponseType(typeof(Response<AtsUserDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] AtsUserUpdateDto user)
        {
            var response = await userService.UpdateUserAsync(id, user);
            return StatusCode((int)response.Code, response);
        }

        [HttpDelete]
        [Route("users/{id}")]
        [ProducesResponseType(typeof(Response<AtsUserDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            var response = await userService.DeleteUserAsync(id);
            return StatusCode((int)response.Code, response);
        }
    }
}
