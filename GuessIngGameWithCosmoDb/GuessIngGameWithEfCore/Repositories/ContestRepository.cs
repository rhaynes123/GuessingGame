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
         * https://www.youtube.com/watch?v=XU1ZuwiWW_k
         * https://stackoverflow.com/questions/71217581/azure-cosmos-db-stored-procedure-select-and-return-multiple-documents-without
         * https://stackoverflow.com/questions/45741570/how-to-save-and-execute-a-stored-procedure-in-cosmos-db-through-azure-portal
         */
        #endregion
        private readonly Container _gameDbcontainer;

        public ContestRepository(CosmosClient cosmosClient,
            string databaseName,
            string containerName)
        {
            _gameDbcontainer = cosmosClient.GetContainer(databaseName, containerName)
                ?? throw new ArgumentNullException(nameof(cosmosClient));
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
        public async Task<(bool, string)> TryUpdateAsync(Contest contest)
        {
            if (contest is null)
            {
                return (false, "No Valid Contest Was Provided");
            }
            try
            {
                ItemResponse<Contest> response = await _gameDbcontainer.UpsertItemAsync(contest, new PartitionKey(contest.Id));
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return (false, "Contest Could Not be Updated In Azure");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
            return (true, string.Empty);
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
        [Obsolete("Migrating to Cosmos so the Id value is now a string and will be generated from a guid")]
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
            string IdAsString = id.ToString();
            return await _gameDbcontainer.ReadItemAsync<Contest>(IdAsString, new PartitionKey(IdAsString));
        }
        public async Task<(bool, Contest, string)> TryGetAsync(Guid id)
        {
            if (id == default)
            {
                return (false, new Contest(), "Invalid Id");
            }
            try
            {
                string IdAsString = id.ToString();
                var response = await _gameDbcontainer.ReadItemAsync<Contest>(IdAsString, new PartitionKey(IdAsString));
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return (false, new Contest(),"Contest Could Not be Found In Azure");
                }
                return (true, response, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, new Contest(), ex.Message);
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            try
            {
                string IdAsString = id.ToString();
                ItemResponse<Contest> response = await _gameDbcontainer.ReadItemAsync<Contest>(IdAsString, new PartitionKey(IdAsString));
                return response.Resource is not null;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
        }

        public async Task<(bool ,string)> TryCheckIfExistsAsync(Guid id)
        {
            try
            {
                if (id == default)
                {
                    return (false, "Invalid Id");
                }
                string IdAsString = id.ToString();
                ItemResponse<Contest> response = await _gameDbcontainer.ReadItemAsync<Contest>(IdAsString, new PartitionKey(IdAsString));
                return (response.StatusCode == HttpStatusCode.OK && response.Resource is not null, string.Empty);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return (false, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, $"UnExpected Exception Encountered. View Exception Message: {ex.Message}");
            }
        }
    }
}

