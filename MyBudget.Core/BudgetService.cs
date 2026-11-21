using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core
{
    public class BudgetService:IBudgetService
    {
        public decimal MonthlyLimit { get;private set; }

        // Stores the monthly budget limit
        public void SetMonthlyLimit(decimal monthlyLimit)
        {
            if (monthlyLimit <= 0)
            {
                throw new InvalidExpenseException("Monthly limit must be greater than ziro.");
            }
            MonthlyLimit =decimal.Round( monthlyLimit,2);
        }

        // Calculates the remaining monthly budget.
        public decimal Remaining(decimal amount)
        {
            decimal remainingAmount = MonthlyLimit - amount;
            return remainingAmount;
        }

        // Determines the budget status based on total spending

        public BudgetStatus Evaluate(decimal totalSpent)
        {
            if (MonthlyLimit == 0)
            {
                return BudgetStatus.NotSet;
            }
            decimal remaningAmount = Remaining(totalSpent);
            if (remaningAmount < 0)
            {
                
                    return BudgetStatus.OverBudget;
             }
            if (remaningAmount < (MonthlyLimit * 0.10m))
            {
                return BudgetStatus.AlmostOut;
            }

            return BudgetStatus.OnTrack;
        }
    }
}
