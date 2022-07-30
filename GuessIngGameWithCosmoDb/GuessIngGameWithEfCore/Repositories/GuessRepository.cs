using System;
using GuessingGameWithCosmodb.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace GuessingGameWithCosmodb.Repositories
{
    public class GuessRepository: IGuessRepository
    {
        //private readonly GameDbContext _context;
        private readonly Container _gameDbcontainer;
        public GuessRepository(CosmosClient cosmosClient,
            string databaseName,
            string containerName)
        {
            //_context = gameDbContext ?? throw new ArgumentNullException(nameof(gameDbContext));
            _gameDbcontainer = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<bool> AddAsync(Guess guess)
        {
            if(_gameDbcontainer is null ||  guess is null)
            {
                return false;
            }
            await _gameDbcontainer.CreateItemAsync(guess, new PartitionKey(guess.Id));
            return true;
        }

        public async Task<IList<Guess>> GetAllAsync(int id = default, int contestId = default, Guid contestUid = default)
        {
            string queryString = @$"SELECT i.guesses FROM Items i";
            var query = _gameDbcontainer.GetItemQueryIterator<Guess>(new QueryDefinition(queryString));
            var guesses = new List<Guess>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                guesses.AddRange(guesses);
            }
            if (id == default || contestUid == default || contestId == default)
            {
                return  guesses;
            }
            return guesses.Where(g => g.Id == $"{id}"
            || g.Contest.Id == $"{contestId}").ToList();
        }
    }
}

