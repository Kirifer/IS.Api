using Is.Core.Abstraction;
using Is.Core.Filtering;
using Is.Models.Entities.BudgetExpenses;

namespace Is.Domain.Services.Interface
{
    public interface IBudgetExpensesService : IEntityService
    {
        Task<Response<List<IsBudgetExpensesDto>>> GetBudgetExpensesAsync();
        Task<Response<IsBudgetExpensesDto>> GetBudgetExpenseAsync(Guid id);
        Task<Response<IsBudgetExpensesDto>> CreateBudgetExpensesAsync(IsBudgetExpensesCreateDto payload);
        Task<Response<IsBudgetExpensesDto>> UpdateBudgetExpensesAsync(Guid id, IsBudgetExpensesUpdateDto payload);
        Task<Response<IsBudgetExpensesDto>> DeleteBudgetExpensesAsync(Guid id);
    }
}
