using System.Net;
using Is.Core.Filtering;
using Is.Domain.Services.Interface;
using Is.Models;
using Is.Models.Entities.BudgetExpenses;
using Microsoft.AspNetCore.Mvc;
namespace Is.Api.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.InternalServerError)]
    public class BudgetExpensesController : ControllerBase
    {
        private readonly IBudgetExpensesService _budgetExpensesService;
        public BudgetExpensesController(IBudgetExpensesService budgetExpensesService)
        {
            _budgetExpensesService = budgetExpensesService;
        }
        [HttpGet]
        [Route("budget-expenses")]
        [ProducesResponseType(typeof(Response<List<IsBudgetExpensesDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBudgetExpensesAsync()
        {
            var response = await _budgetExpensesService.GetBudgetExpensesAsync();
            return Ok(response);
        }
        [HttpGet]
        [Route("budget-expenses/{id}")]
        public async Task<IActionResult> GetBudgetExpenseAsync(Guid id)
        {
            var response = await _budgetExpensesService.GetBudgetExpenseAsync(id);
            return Ok(response);
        }
        [HttpPost]
        [Route("budget-expenses")]
        public async Task<IActionResult> CreateBudgetExpensesAsync([FromBody] IsBudgetExpensesCreateDto payload)
        {
            var response = await _budgetExpensesService.CreateBudgetExpensesAsync(payload);
            return Ok(response);
        }
        [HttpPut]
        [Route("budget-expenses/{id}")]
        public async Task<IActionResult> UpdateBudgetExpensesAsync(Guid id, [FromBody] IsBudgetExpensesUpdateDto payload)
        {
            var response = await _budgetExpensesService.UpdateBudgetExpensesAsync(id, payload);
            return Ok(response);
        }
        [HttpDelete]
        [Route("budget-expenses/{id}")]
        public async Task<IActionResult> DeleteBudgetExpensesAsync(Guid id)
        {
            var response = await _budgetExpensesService.DeleteBudgetExpensesAsync(id);
            return Ok(response);
        }
    }
    
}
