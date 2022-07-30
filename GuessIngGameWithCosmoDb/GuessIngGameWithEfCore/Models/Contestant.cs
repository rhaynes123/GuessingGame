using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GuessingGameWithCosmodb.Models
{
	[Table("Contestant")]
	public class Contestant
	{
		public Contestant()
		{
		}
		[Key]
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		[ForeignKey("ContestId")]
		[JsonProperty(PropertyName = "contest")]
		public Contest Contest { get; set; }
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }
		[JsonProperty(PropertyName = "email")]
		public string Email { get; set; }
		[JsonProperty(PropertyName = "prizeId")]
		public string? PrizeId { get; set; }

		public bool IsAWinner()
        {
			return string.IsNullOrWhiteSpace(PrizeId);
        }
	}
}

