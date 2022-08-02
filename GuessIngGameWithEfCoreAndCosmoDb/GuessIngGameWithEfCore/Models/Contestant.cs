using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuessIngGameWithEfCoreAndCosmoDb.Models
{
	[Table("Contestant")]
	public class Contestant
	{
		public Contestant()
		{
		}
		[Key]
		public int Id { get; set; }
		[ForeignKey("ContestId")]
		public Contest Contest { get; set; }

		public string Name { get; set; }
		public string Email { get; set; }
		public int? PrizeId { get; set; }

		public bool IsAWinner()
        {
			return PrizeId is not null || PrizeId == default(int);
        }
	}
}

