using System;
using GuessIngGameWithEfCoreAndCosmoDb.Models;
namespace GuessIngGameWithEfCoreAndCosmoDb.DTOs
{
    public record CreateGuessRequest(int Id, int Number, int Contest, CreateContestantRequest? Contestant);
}

