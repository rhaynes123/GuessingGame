using System;
using Newtonsoft.Json;

namespace GuessingGameWithCosmodb.DTOs
{
    public record CreateOrUpdatePrizeRequest(int Id, [JsonProperty("description")] string? Description,int? Place, bool IsWon, int? ContestId);
}

