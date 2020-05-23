using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        ICategoryRepository Category { get; }
        IExpenseDetailRepository ExpenseDetail { get; }
        IExpenseRepository Expense { get; }
        ISourceRepository Source { get; }
        Task SaveAsync();
    }
}
