using System;
using GuessIngGameWithEfCoreAndCosmoDb.Data;
using GuessIngGameWithEfCoreAndCosmoDb.Models;
using Microsoft.EntityFrameworkCore;

namespace GuessIngGameWithEfCoreAndCosmoDb.Repositories
{
    public class ContestRepository: IContestRepository
    {
        #region External Links
        /*
         * https://www.youtube.com/watch?v=5ZU2xA_Y3G8
         * https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-api-dotnet-application
         * https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-api-get-started
         * https://www.youtube.com/watch?v=MicJm1hRNKU
         * https://medium.com/swlh/clean-architecture-with-partitioned-repository-pattern-using-azure-cosmos-db-62241854cbc5
         * https://www.youtube.com/watch?v=XU1ZuwiWW_k
         * https://stackoverflow.com/questions/71217581/azure-cosmos-db-stored-procedure-select-and-return-multiple-documents-without
         * https://stackoverflow.com/questions/45741570/how-to-save-and-execute-a-stored-procedure-in-cosmos-db-through-azure-portal
         * https://docs.microsoft.com/en-us/ef/core/providers/cosmos/?tabs=dotnet-core-cli
         * https://dotnetcoretutorials.com/2020/05/02/using-azure-cosmosdb-with-net-core-part-2-ef-core/
         */
        #endregion
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
            return await _context.Contests.ToListAsync();
        }
        [Obsolete("Moving to a document database and id won't be an int")]
        public async Task<Contest> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Contest> GetAsync(Guid id)
        {
            if (id == default)
            {
                return new Contest();
            }
            return await _context.Contests.Include(c => c.Prizes).FirstAsync(c => c.UId == id);
        }
        [Obsolete("Moving to a document database and id won't be an int")]
        public async Task<bool> ExistsAsync(int Id)
        {
            throw new NotImplementedException();
        }
    }
}

