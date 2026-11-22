using MyBudget.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MyBudget.Data
{
    public class JsonExpenseStore:IExpenseStore
    {
        private readonly string _path;
        public JsonExpenseStore(string path) 
        {
            _path = path;
        }

        // Loads expenses from json file.
        public IReadOnlyList<Expense> Load()
        {
            if (!File.Exists(_path))
            {
                return new List<Expense>();
            }
            string json=File.ReadAllText(_path);
            if(string.IsNullOrWhiteSpace(json))
            {
                return new List<Expense>();
            }
            return JsonSerializer.Deserialize<List<Expense>>(json) ?? new List<Expense>(); 
        }

        // Save Expenses to json file.
        public void Save(IEnumerable<Expense> expenses)
        {
            string json = JsonSerializer.Serialize(expenses,
                new JsonSerializerOptions{WriteIndented = true});
            File.WriteAllText(_path, json);
        }
    }
}
