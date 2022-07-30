using System;
using GuessIngGameWithEfCore.Data;
using GuessIngGameWithEfCore.Models;
using Microsoft.EntityFrameworkCore;

namespace GuessIngGameWithEfCore.Repositories
{
    public class GuessRepository: IGuessRepository
    {
        private readonly GameDbContext _context;
        public GuessRepository(GameDbContext gameDbContext)
        {
            _context = gameDbContext ?? throw new ArgumentNullException(nameof(gameDbContext));
        }

        public async Task<bool> AddAsync(Guess guess)
        {
            if(_context is null || _context.Guesses is null || guess is null)
            {
                return false;
            }
            await _context.Guesses.AddAsync(guess);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<Guess>> GetAllAsync(int id = default, int contestId = default, Guid contestUid = default)
        {
            if(id == default || contestUid == default || contestId == default)
            {
                return await _context.Guesses.ToListAsync();
            }
            return await _context.Guesses.Where(g => g.Id == id
            || g.Contest.Id == contestId
            || g.Contest.UId == contestUid).ToListAsync();
        }
    }
}

