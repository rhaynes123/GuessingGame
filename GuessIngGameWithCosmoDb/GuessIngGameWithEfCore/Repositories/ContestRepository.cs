using System;
using GuessingGameWithCosmodb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using System.Net;

namespace GuessingGameWithCosmodb.Repositories
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
         */
        #endregion
        private readonly Container _gameDbcontainer;

        public ContestRepository(CosmosClient cosmosClient,
            string databaseName,
            string containerName)
        {
            _gameDbcontainer = cosmosClient.GetContainer(databaseName, containerName)
                ?? throw new ArgumentNullException(nameof(_gameDbcontainer));
        }

        public async Task<bool> AddAsync(Contest contest)
        {
            if(contest is null)
            {
                return false;
            }
            try
            {
                ItemResponse<Contest> response = await _gameDbcontainer.CreateItemAsync(contest, new PartitionKey(contest.Id));
                if (response.StatusCode != HttpStatusCode.Created)
                {
                    return false;
                }
            }
            catch(Exception)
            {
              
                return false;
            }
            return true;
        }

        public async Task<(bool, string)> TryAddAsync(Contest contest)
        {
            if (contest is null)
            {
                return (false, "No Valid Contest Was Provided");
            }
            try
            {
                ItemResponse<Contest> response = await _gameDbcontainer.CreateItemAsync(contest, new PartitionKey(contest.Id));
                if (response.StatusCode != HttpStatusCode.Created)
                {
                    return (false, "Contest Could Not be Created In Azure");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
            return (true, string.Empty);
        }
        public async Task<bool> UpdateAsync(Contest contest)
        {
            if (contest is null)
            {
                return false;
            }
            await _gameDbcontainer.UpsertItemAsync(contest, new PartitionKey(contest.Id));
            return true;
        }

        public async Task<IList<Contest>> GetAllAsync()
        {
            if (_gameDbcontainer is null)
            {
                return new List<Contest>();
            }
            string queryString = @$"SELECT * FROM Items";
            var query = _gameDbcontainer.GetItemQueryIterator<Contest>(new QueryDefinition(queryString));
            var Contests = new List<Contest>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                Contests.AddRange(response);
            }
            return Contests;
        }

        public async Task<Contest> GetAsync(int id)
        {
            if(id == default)
            {
                return new Contest();
            }
            return await _gameDbcontainer.ReadItemAsync<Contest>($"{id}", new PartitionKey(id));
        }

        public async Task<Contest> GetAsync(Guid id)
        {
            if (id == default)
            {
                return new Contest();
            }
            return (await GetAllAsync()).First(r => r.Id == id.ToString());
        }

        public async Task<bool> ExistsAsync(int Id)
        {
            throw new NotImplementedException();
        }
    }
}

