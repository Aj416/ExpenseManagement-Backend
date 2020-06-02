using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IExpenseRepository : IRepositoryBase<Expense>
    {
        Task<IEnumerable<Expense>> GetAllExpenseAsync(DateTime date);
        Task<IEnumerable<Expense>> GetExpenseWithDetailsAsync(DateTime date);
        Task<Expense> GetExpenseWithDetailsAsync(int id);
        void CreateExpense(Expense expense);
        void UpdateExpense(Expense expense);
        void DeleteExpense(Expense expense);
        Task<IEnumerable<Expense>> GetMonthWiseExpense(DateTime date);
        
    }
}
