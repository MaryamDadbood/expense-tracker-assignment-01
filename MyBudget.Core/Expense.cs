using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MyBudget.Core
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "kind")]
    [JsonDerivedType(typeof(OneTimeExpense), "onetime")]
    [JsonDerivedType(typeof(RecurringExpense), "recurring")]
    public abstract record Expense(Guid Id, string Description,
        decimal Amount, ExpenseCategory Category, DateOnly Date) : IReportable
    {
        public abstract Decimal MonthlyImpact {  get; }
        public virtual string ToReportLine()
        {
            return $"{Description} {Amount}";
        }
    }
}
