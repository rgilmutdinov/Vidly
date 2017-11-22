using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;

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
			var movies = _context.Movies
				.Include(m => m.Genre)
				.ToList();

			return View(movies);
		}

		[Route("movies/{id}")]
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
	}
}