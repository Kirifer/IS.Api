using Is.Datalayer.Entities;
using Is.Datalayer.Interface;

namespace Is.Datalayer.Implementation
{
    public class BudgetExpensesRepository : BaseRepository<BudgetExpenses>, IBudgetExpensesRepository
    {
        public BudgetExpensesRepository(AtsDbContext context) : base(context)
        {

        }
    }
}
