using System;
namespace GuessingGameWithCosmodb.DTOs
{
    public record CreateContestantRequest(string? Name, string? Email);
}

