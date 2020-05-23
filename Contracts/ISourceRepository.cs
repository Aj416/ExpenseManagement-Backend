using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISourceRepository : IRepositoryBase<Source>
    {
        Task<IEnumerable<Source>> GetAllSourceAsync();
        Task<Source> GetSourceByIdAsync(int id);
        void CreateSource(Source source);
        void UpdateSource(Source source);
        void DeleteSource(Source source);
    }
}
