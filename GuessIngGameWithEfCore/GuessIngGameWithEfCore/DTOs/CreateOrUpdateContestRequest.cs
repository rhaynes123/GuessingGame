using System;
using Newtonsoft.Json;

namespace GuessIngGameWithEfCore.DTOs
{
    public record CreateOrUpdateContestRequest(int Id, Guid UId, string? Name, int WinningNumber, bool Active, [JsonProperty("prizes")]IEnumerable<CreateOrUpdatePrizeRequest>? Prizes);
}

