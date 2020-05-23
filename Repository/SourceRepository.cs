using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class SourceRepository : RepositoryBase<Source>, ISourceRepository
    {
        public SourceRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateSource(Source source)
        {
            Create(source);
        }

        public void DeleteSource(Source source)
        {
            Delete(source);
        }

        public async Task<IEnumerable<Source>> GetAllSourceAsync()
        {
            return await FindAll()
                .OrderBy(c => c.AccountHolderName)
                .ToListAsync();
        }

        public async Task<Source> GetSourceByIdAsync(int id)
        {
            return await FindByCondition(a => a.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public void UpdateSource(Source source)
        {
            Update(source);
        }
    }
}
