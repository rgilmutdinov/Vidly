using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
	public class Movie
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

		public Genre Genre { get; set; }

		[Display(Name = "Genre")]
		[Required]
		public byte GenreId { get; set; }
	}
}