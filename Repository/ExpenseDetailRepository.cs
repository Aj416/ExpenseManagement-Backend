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

        public void CreateExpenseDetail(ExpenseDetail category)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteExpenseDetail(ExpenseDetail category)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ExpenseDetail> ExpenseDetailByCategory(int categoryId)
        {
            return  FindByCondition(a => a.CategoryId.Equals(categoryId)).ToList();
        }

        public IEnumerable<ExpenseDetail> ExpenseDetailBySource(int sourceId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ExpenseDetail>> GetAllExpenseDetailAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<ExpenseDetail> GetExpenseDetailByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateExpenseDetail(ExpenseDetail category)
        {
            throw new System.NotImplementedException();
        }
    }
}
