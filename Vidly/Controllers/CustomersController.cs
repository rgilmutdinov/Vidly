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

			CustomerFormViewModel vm = new CustomerFormViewModel
			{
				Customer        = new Customer(),
				MembershipTypes = membershipTypes
			};

			return View("CustomerForm", vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("customers/save")]
		public ActionResult Save(Customer customer)
		{
			if (!ModelState.IsValid)
			{
				var vm = new CustomerFormViewModel
				{
					Customer = customer,
					MembershipTypes = _context.MembershipTypes.ToList()
				};

				return View("CustomerForm", vm);
			}

			if (customer.Id == 0)
			{
				_context.Customers.Add(customer);
			}
			else
			{
				var dbCustomer = _context.Customers.Single(c => c.Id == customer.Id);

				dbCustomer.Name = customer.Name;
				dbCustomer.Birthdate = customer.Birthdate;
				dbCustomer.MembershipTypeId = customer.MembershipTypeId;
				dbCustomer.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
			}

			_context.SaveChanges();

			return RedirectToAction("Index", "Customers");
		}

		[Route("customers")]
		public ViewResult Index()
		{
			var customers = _context.Customers
				.Include(c => c.MembershipType)
				.ToList();

			return View(customers);
		}

		[Route("customers/{id:int}")]
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

		[Route("customers/edit/{id:int}")]
		public ActionResult Edit(int id)
		{
			Customer customer = _context.Customers
				.SingleOrDefault(c => c.Id == id);

			if (customer == null)
			{
				return HttpNotFound();
			}

			var vm = new CustomerFormViewModel
			{
				Customer        = customer,
				MembershipTypes = _context.MembershipTypes.ToList()
			};

			return View("CustomerForm", vm);
		}
	}
}