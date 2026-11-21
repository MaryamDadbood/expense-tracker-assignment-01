using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core
{
    public record OneTimeExpense(Guid Id, string Description,
        decimal Amount, ExpenseCategory Category, DateOnly Date):Expense(Id,Description,
        Amount,Category, Date)
    {
        public override decimal MonthlyImpact => Amount;
    }
    
}
