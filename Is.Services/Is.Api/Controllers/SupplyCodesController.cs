using System.Net;
using Is.Core.Filtering;
using Is.Domain.Services;
using Is.Domain.Services.Interface;
using Is.Models;
using Is.Models.Entities.SupplyCodes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Is.Api.Controllers
{
    [Authorize()]
    [ApiController]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.InternalServerError)]
    public class SupplyCodesController : ControllerBase
    {
        private readonly ISupplyCodesService _supplyCodesService;

        public SupplyCodesController(ISupplyCodesService supplyCodesService)
        {
            _supplyCodesService = supplyCodesService;
        }
        [HttpGet]
        [Route("supplycodes")]
        [ProducesResponseType(typeof(Response<List<IsSupplyCodesDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSupplyCodesAsync()
        {
            var response = await _supplyCodesService.GetSupplyCodesAsync();
            return StatusCode((int)response.Code, response);
        }

        [HttpGet]
        [Route("supplycodes/{id}")]
        public async Task<IActionResult>GetSupplyCodeAsync(Guid id)
        {
            var response = await _supplyCodesService.GetSupplyCodeAsync(id);
            return StatusCode((int)response.Code, response);
        }

        // HTTP POST
        [HttpPost]
        [Route("supplycodes")]
        public async Task<IActionResult> AddSupplyCodesAsync([FromBody] IsSupplyCodesCreateDto payload)
        {
            var response = await _supplyCodesService.CreateSupplyCodesAsync(payload);
            return StatusCode((int)response.Code,response);
        }

        [HttpPut]
        [Route("supplycodes/{id}")]
        public async Task<IActionResult> UpdateSupplyCodesAsync(Guid id, [FromBody] IsSupplyCodesUpdateDto payload)
        {
            var response = await _supplyCodesService.UpdateSupplyCodesAsync(id, payload);
            return StatusCode((int)response.Code, response);
        }

        [HttpDelete]
        [Route("supplycodes/{id}")]
        public async Task<IActionResult> DeleteSupplyCodesAync(Guid id)
        {
            var response = await _supplyCodesService.DeleteSupplyCodesAsync(id);
            return StatusCode((int)response.Code, response);
        }
    }
}
