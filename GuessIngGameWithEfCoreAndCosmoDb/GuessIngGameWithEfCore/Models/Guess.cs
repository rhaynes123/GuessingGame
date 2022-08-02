using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuessIngGameWithEfCoreAndCosmoDb.Models
{
	[Table("Guess")]
	public class Guess
	{
		public Guess()
		{

		}
		[Key]
		public int Id { get; set; }
		[Required, Range(0, int.MaxValue)]
		public int Number { get; set; }
		[ForeignKey("ContestId")]
		public Contest Contest { get; set; }
		[ForeignKey("ContestantId")]
		public Contestant Contestant { get; set; }
	}
}

