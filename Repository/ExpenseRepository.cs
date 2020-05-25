using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ExpenseRepository : RepositoryBase<Expense>, IExpenseRepository
    {
        public ExpenseRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateExpense(Expense expense)
        {
            Create(expense);
        }

        public void DeleteExpense(Expense expense)
        {
            Delete(expense);
        }

        public async Task<IEnumerable<Expense>> GetAllExpenseAsync()
        {
            return await FindAll()
                .Include(exp => exp.Detail)
                .Where(exp => exp.Date.Month.Equals(DateTime.Now.Month) && exp.Date.Year.Equals(DateTime.Now.Year))
                .OrderByDescending(exp => exp.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetExpenseWithDetailsAsync(DateTime date)
        {
            return await FindByCondition(exp => exp.Date.Day.Equals(date.Day) && exp.Date.Month.Equals(date.Month) && exp.Date.Year.Equals(date.Year))
                .Include(exp => exp.Detail)
                .ToListAsync();
        }

        public async Task<Expense> GetExpenseWithDetailsAsync(int id)
        {
            return await FindByCondition(exp => exp.Id.Equals(id))
                .Include(exp => exp.Detail)
                .FirstOrDefaultAsync();
        }

        public void UpdateExpense(Expense expense)
        {
            Update(expense);
        }
    }
}
