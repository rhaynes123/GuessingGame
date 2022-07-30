using System;
using GuessIngGameWithEfCore.Models;

namespace GuessIngGameWithEfCore.Repositories
{
    public interface IContestRepository
    {
        Task<Contest> GetAsync(int id);
        Task<Contest> GetAsync(Guid id);
        Task<IList<Contest>> GetAllAsync();
        Task<bool> AddAsync(Contest contest);
        Task<bool> UpdateAsync(Contest contest);
        Task<bool> ExistsAsync(int Id);
    }
}

