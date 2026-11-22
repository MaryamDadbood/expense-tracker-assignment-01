using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MyBudget.Core;

namespace MyBudget.Tests
{
    public class CustomExpenseTests
    {
        [Fact]
        public void Remaining_ReturnCorrectAmount()
        {
            // Arrange
            var budget = new BudgetService();
            budget.SetMonthlyLimit(1000);
            decimal totalSpent = 300;

            // Act
            decimal result = budget.Remaining(totalSpent);

            //Assert
            Assert.Equal(700m, result);
        }

        [Theory]
        [InlineData(1000,200,800)]
        [InlineData(1000,1000,0)]
        [InlineData(1000,1200,-200)]

        public void Remaining_WithDifferentAmounts(int limit,int totalSpent, int expected)
        {
            // Arrange
            var budget = new BudgetService();
            budget.SetMonthlyLimit(limit);

            // Act
            decimal result= budget.Remaining(totalSpent);

            // Assert
            Assert.Equal((decimal)expected, result);
        }

        [Fact]
        public void Evaluate_ReturnsOverBudget()
        {
            // Arrange
            var budget = new BudgetService();
            budget.SetMonthlyLimit(1000);
            decimal totalSpent = 1200;

            //Act
            BudgetStatus result= budget.Evaluate(totalSpent);

            // Assert
            Assert.Equal(BudgetStatus.OverBudget, result);
        }
    }
    
}
