using Contracts;
using Entities;
using System;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private ICategoryRepository _category;
        private IExpenseDetailRepository _expenseDetail;
        private IExpenseRepository _expense;
        private ISourceRepository _source;

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_repoContext);
                }
                return _category;
            }
        }

        public IExpenseDetailRepository ExpenseDetail
        {
            get
            {
                if (_expenseDetail == null)
                {
                    _expenseDetail = new ExpenseDetailRepository(_repoContext);
                }
                return _expenseDetail;
            }
        }

        public IExpenseRepository Expense
        {
            get
            {
                if (_expense == null)
                {
                    _expense = new ExpenseRepository(_repoContext);
                }
                return _expense;
            }
        }

        public ISourceRepository Source
        {
            get
            {
                if (_source == null)
                {
                    _source = new SourceRepository(_repoContext);
                }
                return _source;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}
