using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
	public class MoviesController : ApiController
	{
		private ApplicationDbContext _context;

		public MoviesController()
		{
			_context = new ApplicationDbContext();
		}

		// GET /api/movies
		public IHttpActionResult GetMovies(string query = null)
		{
			var moviesQuery = _context.Movies
				.Include(m => m.Genre)
				.Where(m => m.NumberAvailable > 0);

			if (!string.IsNullOrWhiteSpace(query))
			{
				moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
			}

			var movies = moviesQuery
				.ToList()
				.Select(Mapper.Map<Movie, MovieDto>);

			return Ok(movies);
		}

		// GET /api/movies/id
		public IHttpActionResult GetMovie(int id)
		{
			var movie = _context.Movies
				.SingleOrDefault(m => m.Id == id);

			if (movie == null)
			{
				return NotFound();
			}

			return Ok(Mapper.Map<Movie, MovieDto>(movie));
		}

		// POST /api/movies
		[HttpPost]
		public IHttpActionResult CreateMovie(MovieDto movieDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			Movie movie = Mapper.Map<MovieDto, Movie>(movieDto);
			movie.DateAdded = DateTime.Now;
			movie.NumberAvailable = movie.NumberInStock;

			_context.Movies.Add(movie);
			_context.SaveChanges();

			movieDto.Id = movie.Id;
			
			return Created(
				new Uri(Request.RequestUri + "/" + movie.Id),
				movieDto
			);
		}

		// PUT /api/movies/id
		[HttpPut]
		public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var dbMovie = _context.Movies
				.SingleOrDefault(m => m.Id == id);

			if (dbMovie == null)
			{
				return NotFound();
			}

			Mapper.Map(movieDto, dbMovie);

			_context.SaveChanges();

			return Ok();
		}

		// DELETE /api/movies/id
		[HttpDelete]
		public IHttpActionResult DeleteMovie(int id)
		{
			var dbMovie = _context.Movies
				.SingleOrDefault(m => m.Id == id);

			if (dbMovie == null)
			{
				return NotFound();
			}

			_context.Movies.Remove(dbMovie);
			_context.SaveChanges();

			return Ok();
		}
	}
}
