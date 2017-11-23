using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
	public class CustomersController : Controller
	{
		private ApplicationDbContext _context;

		public CustomersController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		[Route("customers/new")]
		public ActionResult New()
		{
			var membershipTypes = _context.MembershipTypes
				.ToList();

			NewCustomerViewModel vm = new NewCustomerViewModel
			{
				MembershipTypes = membershipTypes
			};

			return View(vm);
		}

		[Route("customers")]
		public ViewResult Index()
		{
			var customers = _context.Customers
				.Include(c => c.MembershipType)
				.ToList();

			return View(customers);
		}

		[Route("customers/{id}")]
		public ActionResult Details(int id)
		{
			Customer customer = _context.Customers
				.Include(c => c.MembershipType)
				.SingleOrDefault(c => c.Id == id);

			if (customer == null)
			{
				return HttpNotFound();
			}

			return View(customer);
		}
	}
}