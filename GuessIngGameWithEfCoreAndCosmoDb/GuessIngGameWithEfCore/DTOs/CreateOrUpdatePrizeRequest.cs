using System;
using Newtonsoft.Json;

namespace GuessIngGameWithEfCoreAndCosmoDb.DTOs
{
    public record CreateOrUpdatePrizeRequest(int Id, [JsonProperty("description")] string? Description,int? Place, bool IsWon, int? ContestId);
}

