using System;
namespace GuessIngGameWithEfCoreAndCosmoDb.DTOs
{
    public record CreateContestantRequest(string? Name, string? Email);
}

