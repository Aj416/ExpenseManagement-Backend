using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IExpenseDetailRepository : IRepositoryBase<ExpenseDetail>
    {
        Task<IEnumerable<ExpenseDetail>> GetAllExpenseDetailAsync();
        Task<ExpenseDetail> GetExpenseDetailByIdAsync(int id);
        IEnumerable<ExpenseDetail> ExpenseDetailByCategory(int categoryId);
        IEnumerable<ExpenseDetail> ExpenseDetailBySource(int sourceId);
        void CreateExpenseDetail(ExpenseDetail category);
        void UpdateExpenseDetail(ExpenseDetail category);
        void DeleteExpenseDetail(ExpenseDetail category);
    }
}
