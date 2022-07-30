using System;
using GuessIngGameWithEfCore.Data;
using GuessIngGameWithEfCore.Models;
using Microsoft.EntityFrameworkCore;

namespace GuessIngGameWithEfCore.Repositories
{
    public class ContestRepository: IContestRepository
    {
        private readonly GameDbContext _context;
        public ContestRepository(GameDbContext gameDbContext)
        {
            _context = gameDbContext ?? throw new ArgumentNullException(nameof(gameDbContext));
        }

        public async Task<bool> AddAsync(Contest contest)
        {
            if(contest is null)
            {
                return false;
            }
            await _context.Contests.AddAsync(contest);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(Contest contest)
        {
            if (contest is null)
            {
                return false;
            }
            _context.Contests.Update(contest);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<Contest>> GetAllAsync()
        {
            if (_context is null || _context.Contests is null)
            {
                return new List<Contest>();
            }
            return await _context.Contests.Include(c => c.Prizes).ToListAsync();
        }

        public async Task<Contest> GetAsync(int id)
        {
            if(id == default)
            {
                return new Contest();
            }
            return await _context.Contests.Include(c => c.Prizes).FirstAsync(c => c.Id == id);
        }

        public async Task<Contest> GetAsync(Guid id)
        {
            if (id == default)
            {
                return new Contest();
            }
            return await _context.Contests.Include(c => c.Prizes).FirstAsync(c => c.UId == id);
        }

        public async Task<bool> ExistsAsync(int Id)
        {
            return await _context.Contests.AnyAsync(c =>c.Id == Id);
        }
    }
}

