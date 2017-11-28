﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
	public class MovieDto
	{
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		[Required]
		public DateTime ReleaseDate { get; set; }

		public DateTime DateAdded { get; set; }

		[Required]
		[Range(1, 20)]
		public byte NumberInStock { get; set; }

		[Display(Name = "Genre")]
		[Required]
		public byte GenreId { get; set; }
	}
}