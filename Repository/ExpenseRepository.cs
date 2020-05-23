using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class ExpenseRepository : RepositoryBase<Expense>, IExpenseRepository
    {
        public ExpenseRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
