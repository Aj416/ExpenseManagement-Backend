using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IExpenseRepository : IRepositoryBase<Expense>
    {
        Task<IEnumerable<Expense>> GetAllExpenseAsync();

        Task<IEnumerable<Expense>> GetExpenseWithDetailsAsync(DateTime date);
    }
}
