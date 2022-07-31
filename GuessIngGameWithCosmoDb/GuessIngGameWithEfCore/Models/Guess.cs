using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GuessingGameWithCosmodb.DTOs;
using Newtonsoft.Json;

namespace GuessingGameWithCosmodb.Models
{
	[Table("Guess")]
	public class Guess
	{
		public Guess()
		{

		}
		public Guess(CreateGuessRequest guess)
		{
			Id = $"{guess.Id}";
			Number = guess.Number;
			Contestant = new Contestant
			{
				Name = string.IsNullOrWhiteSpace(guess.Contestant.Name) ? throw new ArgumentException("name") : guess.Contestant.Name,
				Email = string.IsNullOrWhiteSpace(guess.Contestant.Email) ? throw new ArgumentException("email") : guess.Contestant.Email,
				
			};
		}
		[Key]
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		[Required, Range(0, int.MaxValue)]
		[JsonProperty(PropertyName = "number")]
		public int Number { get; set; }
		[ForeignKey("ContestId")]
		[JsonProperty(PropertyName = "contest")]
		public Contest Contest { get; set; }
		[ForeignKey("ContestantId")]
		[JsonProperty(PropertyName = "contestant")]
		public Contestant Contestant { get; set; }
	}
}

