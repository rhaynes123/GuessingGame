using System;
using GuessingGameWithCosmodb.Models;
namespace GuessingGameWithCosmodb.DTOs
{
    public record CreateGuessRequest(int Id, int Number, int Contest, CreateContestantRequest? Contestant);
}

