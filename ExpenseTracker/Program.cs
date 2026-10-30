// =====================================================================
//  Program.cs  —  the interactive console UI for MyBudget (Assignment 1).
//  Target framework: .NET 10 (LTS), language C# 14.
//
//  >>> BUILD THE MENU-DRIVEN UI HERE (Modules 1-3). <<<
//
//  Once you have implemented BudgetRules.cs (so the unit tests pass), wire it
//  up to a console interface that meets the assignment brief:
//
//    * Print a banner (try a raw string literal).
//    * Loop a menu until the user exits, using a switch on the choice:
//        1) Add an expense   2) View summary   3) Set monthly budget   4) Exit
//    * Read and VALIDATE input, re-prompting on bad data (decimal.TryParse,
//      BudgetRules.NormalizeCategory, a date parse, non-empty text).
//    * Keep running totals in simple variables (no collections / no classes).
//    * Use BudgetRules.ValidateAmount / ClassifyAmount / BudgetStatus /
//      FormatCurrency for all logic and formatting.
//    * Handle bad input with try / catch / finally and InvalidExpenseException.
//
//  See section 6 of the assignment brief for a sample run to aim for.
// =====================================================================
using ExpenseTracker;
using System.ComponentModel;
using System.ComponentModel.Design;
decimal totalSpent=0m;
int expenseCount=0;
decimal highestExpense=0m;
decimal foodTotal=0m;
decimal transitTotal=0m;
decimal utilityTotal=0m;
decimal entertainmentTotal=0m;
decimal otherTotal=0m;
decimal monthlyBudget=0m;
bool budgetIsSet=false;


Console.WriteLine("===============================");
Console.WriteLine("   MyBudget Expense Tracker  ");
Console.WriteLine("===============================");
while (true)
{
     
    Console.WriteLine("1. Add an Expense");
    Console.WriteLine("2. View summary");
    Console.WriteLine("3. Set Monthly Budget"); 
    Console.WriteLine("4. Exit");
    Console.WriteLine("");
    Console.Write("Enter your choice: ");
    string strChoice=Console.ReadLine();


    switch (strChoice)
    {
        case "1":
            string? note;
            decimal amount;
            string? category;
            string date;
            DateTime expenseDate;
            string sizeBand;
            string expenseDescription;

            // Prompt for a non-empty expense description

            while (true)
            {
                Console.Write("Enter description: ");
                string? description = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(description))
                {
                    expenseDescription = description;
                    break;
                }
                else
                {
                    Console.WriteLine("Description can not be empty. Please try again.");
                }

            }

            // keep asking for an amount until valid input is entered
            while (true)
            {

                Console.Write("Enter expense amount: ");
                
                string strAmount = Console.ReadLine();

                // check whether the input can be converted to a decimal.
                if (decimal.TryParse(strAmount, out amount))
                {

                    try
                    {
                        // Validate the amount and round it to two decimal places.
                        amount = BudgetRules.ValidateAmount(amount);

                       
                        
                        break;
                    }

                    // Handle invalid amount without stopping the program
                    catch (InvalidExpenseException)
                    {
                        Console.WriteLine("Amount must be greater than ziro and not exceeding 1,000,000. Please try again...");
                        Console.WriteLine();
                    }

                }
                else
                {
                    Console.WriteLine("Invalid amount. Please enter a valid amount.");
                    Console.WriteLine();
                }

            }

            // keeping asking a category until valid input is entered
            while (true)
            {
                Console.Write("Enter expense category(Food/Transport/Utilities/Entertainment/Other): ");
                string input = Console.ReadLine();
                Console.WriteLine();
                category = BudgetRules.NormalizeCategory(input);
                if (category == null)
                {
                    Console.WriteLine("Invalid category. Please enter a valid category.");
                }
                else
                {
                    break;
                }
            }


            // Allow the user to enter an optional note
            Console.Write("Enter a note (optional): ");
            note=Console.ReadLine();

            // Get the expense Date and validate that it is not in the future
            while (true)
            { 

                Console.Write("Enter date ('yyyy-mm-dd'): ");
                date= Console.ReadLine();
                

              if (string.IsNullOrWhiteSpace(date)) 
               {
                 expenseDate= DateTime.Today;
                 break;
              }
                if (!DateTime.TryParseExact(date, "yyyy-mm-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out expenseDate))
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                    continue;
                }

                // Check for future date
                    if (expenseDate > DateTime.Today)
                    {
                        Console.WriteLine("Future date are not allowed. Please enter a valid date.");
                    continue;
                    }

                // Valid date :exit the Loop 

                break;
                

            }

            // add expence amount to the matching category total
            switch (category)
            {
                case "Food":
                    foodTotal += amount;
                    break;
                case "Transport":
                    transitTotal += amount;
                    break;
                case "Utilities":
                    utilityTotal += amount;
                    break;
                case "Entertainment":
                    entertainmentTotal += amount;
                    break;
                case "Other":
                    otherTotal += amount;
                    break;
            }
            totalSpent += amount;

            if (amount > highestExpense)
            {
                highestExpense = amount;
            }
            expenseCount++;

            sizeBand =BudgetRules.ClassifyAmount(amount);
            Console.WriteLine("Expense recorded successfully! ");
            Console.WriteLine("");
            Console.WriteLine($"Description:   {expenseDescription}");
            Console.WriteLine($"Amount:        {BudgetRules.FormatCurrency(amount)}");
            Console.WriteLine($"Category:      {category}");
            Console.WriteLine($"Date:          {expenseDate:yyyy-MM-dd}");
            if (!string.IsNullOrWhiteSpace(note))
            {
                Console.WriteLine($"Note:           {note}");
            }
            Console.WriteLine($"Size Band:     {sizeBand}");
            Console.WriteLine();

            break;


            


        case "2":
            Console.WriteLine("=======================================");
            Console.WriteLine("        MONTHLY EXPENDE SUMMERY         ");
            Console.WriteLine("========================================");
            
                Console.WriteLine($"Number of Expenses :         {expenseCount}");
                String totalSpent1=BudgetRules.FormatCurrency(totalSpent);
                Console.WriteLine($"Total Spent:                 {totalSpent1}");

                decimal aveExpense = 0m;
                if (expenseCount > 0)
                {
                    aveExpense = totalSpent / expenseCount;
                }
                string strAveExpense=BudgetRules.FormatCurrency(aveExpense);
                Console.WriteLine($"Average Expense:             {strAveExpense}");
                string strHighestExpense = BudgetRules.FormatCurrency(highestExpense);
                Console.WriteLine($"Highest Single Expense:      {strHighestExpense}");

                Console.WriteLine("---------------------------------------");
                Console.WriteLine("BY CATEGORY");
                Console.WriteLine("");
                string strFoodTotal=BudgetRules.FormatCurrency(foodTotal);
                Console.WriteLine($"Food                       {strFoodTotal}");
                string strTransitTotal=BudgetRules.FormatCurrency(transitTotal);
                Console.WriteLine($"Transport                  {strTransitTotal}");
                string strEntertainmentTotal = BudgetRules.FormatCurrency(entertainmentTotal);
                Console.WriteLine($"Entertainment              {strEntertainmentTotal}");
                string strUtilityTotal=BudgetRules.FormatCurrency(utilityTotal);
                Console.WriteLine($"Utilities                  {strUtilityTotal}");
                string strOtherTotal = BudgetRules.FormatCurrency(otherTotal);
                Console.WriteLine($"Other                      {strOtherTotal}");
                Console.WriteLine("----------------------------------------");
            if (budgetIsSet)
            {
                Console.WriteLine("");
                Console.WriteLine("BUDGET STATUS");
                Console.WriteLine($"MONTHLY BUDGET            {BudgetRules.FormatCurrency(monthlyBudget)}");

                decimal remainingBodget = monthlyBudget - totalSpent;
                string budgetStatus = BudgetRules.BudgetStatus(remainingBodget, monthlyBudget);
                Console.WriteLine($"Remaining Budget          {BudgetRules.FormatCurrency(remainingBodget)}");
                Console.WriteLine($"Budget Status:            {budgetStatus}");
                Console.WriteLine();
                
                
                
            }
            else
            {
                Console.WriteLine("Monthly budget has not been set yet.");
            }


            break;
        case "3":

            while (true)
            {
                Console.Write("Enter monthly budget: ");
                string strinput = Console.ReadLine();
                if (decimal.TryParse(strinput, out monthlyBudget))
                {
                    try
                    {
                        //Validate the amount and round it to two decimal places.
                        monthlyBudget = BudgetRules.ValidateAmount(monthlyBudget);
                        break;
                    }

                    //Handle invalid amount without stopping the program
                    catch (InvalidExpenseException)
                    {
                        Console.WriteLine("Amount must be greater than ziro and not exceeding 1,000,000. Please try again...");
                        Console.WriteLine();
                    }

                }
                else
                {
                    Console.WriteLine("Invalid amount. Please enter a valid amount.");
                    Console.WriteLine();
                }

            }
            budgetIsSet=true;
            break;
        case "4":
            Console.WriteLine("Thank you for using MyBudget Expense Tracker. Goodbye!");
            return;
            

        default:
            Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
            Console.WriteLine();
            break;
    }
}