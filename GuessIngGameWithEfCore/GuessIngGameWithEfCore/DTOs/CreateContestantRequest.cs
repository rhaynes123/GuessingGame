using System;
namespace GuessIngGameWithEfCore.DTOs
{
    public record CreateContestantRequest(string? Name, string? Email);
}

