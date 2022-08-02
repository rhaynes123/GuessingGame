using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GuessIngGameWithEfCoreAndCosmoDb.DTOs;
using Newtonsoft.Json;
using GuessIngGameWithEfCoreAndCosmoDb.Models.Enums;
namespace GuessIngGameWithEfCoreAndCosmoDb.Models
{
	[Table("Contest")]
	public class Contest
	{
		public Contest()
		{

		}
		public Contest(CreateOrUpdateContestRequest contest)
		{
			if(contest is null)
            {
				throw new ArgumentNullException("This constructor can not be called on a null contest");
            }

			Id = contest.UId.ToString();
            WinningNumber = contest.WinningNumber;
			Name = contest.Name;
			Active = contest.Active;
			Prizes = contest.Prizes is null ? new List<Prize>(): contest.Prizes.Select(p => new Prize
			{
				Id = p.Id,
				Description = p.Description,
				Place = (Place)p.Place
			}).ToList();

		}
		[Key]
		public string Id { get; set; }
		[Required]
		public Guid UId { get; set; }
		[Required]
		[JsonProperty("name")]
		public string? Name { get; set; }
        [Required]
        public int WinningNumber { get; set; }

        public bool Active { get; set; }

		public ICollection<Guess> Guesses { get; set; } = new List<Guess>();

		public ICollection<Contestant> Contestants { get; set; } = new List<Contestant>();

		public ICollection<Prize> Prizes { get; set; } = new List<Prize>();

		public void Play(Guess guess)
        {
			Guesses.Add(guess);
			Contestants.Add(guess.Contestant);
			if(Prizes is null || !Prizes.Any())
            {
				throw new InvalidDataException("Play Cannot Be Called On Contests With No Prizes");
            }
			var activePrizes = Prizes.Where(p => p.IsWon == false);
			foreach(var prize in activePrizes)
            {
				switch(prize.Place)
                {
					case Place.First:
                        if (WinningNumber == guess.Number)
                        {
							prize.Won();
							guess.Contestant.PrizeId = prize.Id;
						}
						break;
					case Place.Second:
						if (WinByRange(input:guess.Number, target: WinningNumber, range: 10))
						{
							prize.Won();
							guess.Contestant.PrizeId = prize.Id;
						}
						break;
					case Place.Third:
						if (WinByRange(input: guess.Number, target: WinningNumber, range: 20))
						{
							prize.Won();
							guess.Contestant.PrizeId = prize.Id;
						}
						break;
					default:
						throw new Exception("Unsupported Place");
                }
                
            }
			
        }

		private static bool WinByRange(int input, int target, int range)
        {
			return input > target - range || input < target + range;
        }
    }
}

