using System.Net;

using Is.Core.Filtering;
using Is.Domain.Services.Interface;
using Is.Models;
using Is.Models.Entities.Supply;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Is.Api.Controllers
{
    [Authorize()]
    [ApiController]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.InternalServerError)]
    public class SuppliesController(ISuppliesService suppliesService) : ControllerBase
    {
        private readonly ISuppliesService suppliesService = suppliesService;

        [HttpGet]
        [Route("supplies")]
        [ProducesResponseType(typeof(Response<List<IsSuppliesDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSuppliesAsync()
        {
            var response = await suppliesService.GetSuppliesAsync();
            return StatusCode((int)response.Code, response);
        }

        // FILTER DTO
        [HttpGet]
        [Route("supplies/{id}")]
        public async Task<IActionResult> GetSupplyAsync(Guid id)
        {
            var response = await suppliesService.GetSupplyAsync(id);
            return StatusCode((int)response.Code, response);
        }

        // POST
        [HttpPost]
        [Route("supplies")]
        public async Task<IActionResult> AddSuppliesAsync([FromBody] IsSuppliesCreateDto payload)
        {
            var response = await suppliesService.CreateSuppliesAsync(payload);
            return StatusCode((int)response.Code,response);
        }

        // PUT
        [HttpPut]
        [Route("supplies/{id}")]
        public async Task<IActionResult> UpdateSuppliesAsync(Guid id, [FromBody] IsSuppliesUpdateDto payload)
        {
            var response = await suppliesService.UpdateSuppliesAsync(id, payload);
            return StatusCode((int)response.Code, response);
        }

        // DELETE
        [HttpDelete]
        [Route("supplies/{id}")]
        public async Task<IActionResult> DeleteSupplyAsync(Guid id)
        {
            var response = await suppliesService.DeleteSuppliesAsync(id);
            return StatusCode((int)response.Code, response);
        }
    }


}
