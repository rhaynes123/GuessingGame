using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GuessIngGameWithEfCore.Models
{
	[Table("Prize")]
	public class Prize
	{
		public Prize()
		{
		}

		public int Id { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
		public Enums.Place Place {get;set;}
		public bool IsWon { get; private set; } = false;
		[ForeignKey("ContestId")]
		public Contest Contest { get; set; } 

		public void Won()
        {
			IsWon = true;
        }
	}
}

