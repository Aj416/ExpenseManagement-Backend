using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateCategory(Category category)
        {
            Create(category);
        }

        public void DeleteCategory(Category category)
        {
            Delete(category);
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await FindAll()
                .OrderBy(c => c.Id)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await FindByCondition(a => a.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public void UpdateCategory(Category category)
        {
            Update(category);
        }
    }
}
