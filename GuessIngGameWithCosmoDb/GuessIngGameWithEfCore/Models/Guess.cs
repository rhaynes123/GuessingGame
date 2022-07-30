using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GuessingGameWithCosmodb.Models
{
	[Table("Guess")]
	public class Guess
	{
		public Guess()
		{

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

