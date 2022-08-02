using System;
using GuessIngGameWithEfCoreAndCosmoDb.Models;

namespace GuessIngGameWithEfCoreAndCosmoDb.Repositories
{
    public interface IGuessRepository
    {
        Task<IList<Guess>> GetAllAsync(int id = default, int contestId = default, Guid contestUid = default);
        Task<bool> AddAsync(Guess guess);
    }
}

