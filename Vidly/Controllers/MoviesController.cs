using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
	public class MoviesController : Controller
	{
		private ApplicationDbContext _context;

		public MoviesController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		[Route("movies")]
		public ViewResult Index()
		{
			return View();
		}

		[Route("movies/{id:int}")]
		public ActionResult Details(int id)
		{
			Movie movie = _context.Movies
				.Include(c => c.Genre)
				.SingleOrDefault(c => c.Id == id);

			if (movie == null)
			{
				return HttpNotFound();
			}

			return View(movie);
		}

		[Route("movies/edit/{id:int}")]
		public ActionResult Edit(int id)
		{
			Movie movie = _context.Movies
				.SingleOrDefault(m => m.Id == id);

			if (movie == null)
			{
				return HttpNotFound();
			}

			var vm = new MovieFormViewModel(movie)
			{
				Genres = _context.Genres.ToList()
			};

			return View("MovieForm", vm);
		}

		[Route("movies/new")]
		public ActionResult New()
		{
			var genres = _context.Genres
				.ToList();

			MovieFormViewModel vm = new MovieFormViewModel(new Movie())
			{
				Genres = genres
			};

			return View("MovieForm", vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("movies/save")]
		public ActionResult Save(Movie movie)
		{
			if (!ModelState.IsValid)
			{
				var vm = new MovieFormViewModel(movie)
				{
					Genres = _context.Genres.ToList()
				};

				return View("MovieForm", vm);
			}

			if (movie.Id == 0)
			{
				movie.DateAdded = DateTime.Now;
				_context.Movies.Add(movie);
			}
			else
			{
				var dbMovie = _context.Movies.Single(m => m.Id == movie.Id);

				dbMovie.Name = movie.Name;
				dbMovie.GenreId = movie.GenreId;
				dbMovie.ReleaseDate = movie.ReleaseDate;
				dbMovie.NumberInStock = movie.NumberInStock;
			}

			_context.SaveChanges();

			return RedirectToAction("Index", "Movies");
		}
	}
}