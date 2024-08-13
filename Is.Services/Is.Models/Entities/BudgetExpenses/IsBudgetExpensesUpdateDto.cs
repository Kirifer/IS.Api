using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Is.Models.Entities.BudgetExpenses
{
    public class IsBudgetExpensesUpdateDto
    {
        public Guid Id { get; set; }
        public string MonthCreated { get; set; }
        public int YearCreated { get; set; }
        public decimal Budget { get; set; }
        public decimal Expenses { get; set; }
        public decimal TotalBudget { get; set; }
    }
}
