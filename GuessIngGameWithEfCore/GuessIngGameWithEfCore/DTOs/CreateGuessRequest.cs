using System;
using GuessIngGameWithEfCore.Models;
namespace GuessIngGameWithEfCore.DTOs
{
    public record CreateGuessRequest(int Id, int Number, int Contest, CreateContestantRequest? Contestant);
}

