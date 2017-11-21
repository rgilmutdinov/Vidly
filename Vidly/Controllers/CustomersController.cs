using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
	public class CustomersController : Controller
	{
		readonly List<Customer> _customers = new List<Customer>
		{
			new Customer { Id = 1, Name = "John Smith" },
			new Customer { Id = 2, Name = "Marry Williams" }
		};

		[Route("customers")]
		public ViewResult Index()
		{
			return View(this._customers);
		}

		[Route("customers/{id}")]
		public ActionResult Details(int id)
		{
			Customer customer = this._customers.FirstOrDefault(c => c.Id == id);
			if (customer == null)
			{
				return HttpNotFound();
			}

			return View(customer);
		}
	}
}