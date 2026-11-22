using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core
{
    public class ExpenseRepository:IExpenseRepository
    {
        // Stores all expenses in memory
        private List<Expense> _expenses;

        //Provide access to expense storage
        private readonly IExpenseStore _store;

        //Initializes the repository and loads existing expenses
        public ExpenseRepository(IExpenseStore store)
        {
            _store = store;
            _expenses = _store.Load().ToList();
        }

        // Returns all expenses ordered by date.
        public IReadOnlyList<Expense> GetAll()
        {
            return _expenses.OrderBy(e => e.Date).ToList();
        }

        // Adds a new expense to the List
        public void Add(Expense expense) 
        { 
            if(expense == null)
            {
                throw new ArgumentNullException("expense");
            }
            _expenses.Add(expense);
        }

        // Calculate the total monthly expense.
        public decimal Total()
        {
            return _expenses.Sum(e => e.MonthlyImpact);
        }

        // Calculate the total expenses for each Category.
        public IReadOnlyDictionary<ExpenseCategory,decimal> TotalsByCategory()
        {
            return _expenses
                .GroupBy(e => e.Category)
                .ToDictionary(g => g.Key , g => g.Sum(e => e.MonthlyImpact));
        }

        // Returns expense in the specified category.
        public IReadOnlyList<Expense> InCategory(ExpenseCategory category)
        {
            return _expenses
                .Where(e => e.Category == category)
                .OrderBy(e => e.Date)
                .ToList();
        }

        // Saves all expenses to storage.
        public void Save()
        {
            _store.Save(_expenses);
        }
    }
}
