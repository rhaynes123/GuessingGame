﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GuessingGameWithCosmodb.Models
{
	[Table("Prize")]
	public class Prize
	{
		public Prize()
		{
			
		}
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		[JsonProperty("description")]
		public string Description { get; set; }
		[JsonProperty(PropertyName = "place")]
		public Enums.Place Place {get;set;}
		[JsonProperty(PropertyName = "isWon")]
		public bool IsWon { get; private set; } = false;
		[ForeignKey("ContestId")]
		[JsonProperty(PropertyName = "contest")]
		public Contest Contest { get; set; } 

		public void Won()
        {
			IsWon = true;
        }
	}
}

