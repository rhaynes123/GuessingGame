using System;
using GuessIngGameWithEfCore.Models;

namespace GuessIngGameWithEfCore.Repositories
{
    public interface IGuessRepository
    {
        Task<IList<Guess>> GetAllAsync(int id = default, int contestId = default, Guid contestUid = default);
        Task<bool> AddAsync(Guess guess);
    }
}

