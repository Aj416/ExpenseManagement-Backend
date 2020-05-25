using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ExpenseDetailRepository : RepositoryBase<ExpenseDetail>, IExpenseDetailRepository
    {
        public ExpenseDetailRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void DeleteExpenseDetail(ExpenseDetail expenseDetail)
        {
            Delete(expenseDetail);
        }

        public IEnumerable<ExpenseDetail> ExpenseDetailByCategory(int categoryId)
        {
            return  FindByCondition(a => a.CategoryId.Equals(categoryId)).ToList();
        }

        public IEnumerable<ExpenseDetail> ExpenseDetailBySource(int sourceId)
        {
            return FindByCondition(a => a.SourceId.Equals(sourceId)).ToList();
        }

        public async Task<ExpenseDetail> GetExpenseDetailByIdAsync(int id)
        {
            return await FindByCondition(exdet => exdet.Id.Equals(id))
                .FirstOrDefaultAsync();
        }
    }
}
