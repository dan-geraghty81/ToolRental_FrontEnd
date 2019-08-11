using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ToolRental_FrontEnd.Models;
using ToolRental_FrontEnd.ViewModels;

namespace ToolRental_FrontEnd.Controllers
{
    public class RentalController : Controller
    {
		// GET: Rental
		public ActionResult Index()
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Rentals").Result;
			IEnumerable<Rental> rentals = response.Content.ReadAsAsync<IEnumerable<Rental>>().Result;

			response = WebClient.ApiClient.GetAsync("Customers").Result;
			IList<Customer> customers = response.Content.ReadAsAsync<IList<Customer>>().Result;

			var rentalListViewModel = rentals.Select(r => new RentalListViewModel
			{
				RentalID = r.RentalID,
				DateRented = r.DateRented,
				DateReturned = r.DateReturned,
				CustomerName = customers.Where(c => c.CustomerID == r.CustomerID).Select(u => u.CustomerName).FirstOrDefault()
			}).OrderByDescending(o => o.DateRented).ToList();

			return View(rentalListViewModel);
		}

		// GET: Rental/Details/5
		public ActionResult Details(int Id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Rentals/{Id}").Result;
			var rental = response.Content.ReadAsAsync<Rental>().Result;

			response = WebClient.ApiClient.GetAsync("Customers").Result;
			IList<Customer> customers = response.Content.ReadAsAsync<IList<Customer>>().Result;

			response = WebClient.ApiClient.GetAsync("Movies").Result;
			IList<Tool> tools = response.Content.ReadAsAsync<IList<Tool>>().Result;

			var customerRentalDetails = new CustomerRentalDetailsViewModel
			{
				Rental = rental,
				CustomerName = customers.Where(cu => cu.CustomerID == rental.CustomerID).Select(c => c.CustomerName).FirstOrDefault(),
				RentedTools = rental.RentalItems.Select(ri => new CustomerToolsViewModel
				{
					RentalID = ri.RentalId,
					ToolName = tools.Where(m => m.ToolID == ri.ToolID).Select(s => s.ToolName).FirstOrDefault()
				}).ToList()
			};

			return View(customerRentalDetails);
		}

		// GET: Rental/Create
		public ActionResult Create()
		{
			var rental = new Rental();
			//HttpResponseMessage response = WebClient.ApiClient.GetAsync();

			// Setting the primary key value to a negative value will make SQL server 
			// to find next available primary key value upon saving
			rental.RentalID = -999;
			rental.DateRented = DateTime.Now;
			rental.Customers = GetCustomers();

			return View(rental);
		}

		// POST: Rental/Create
		[HttpPost]
		public ActionResult Create(Rental rental)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("Rentals", rental).Result;
				rental = response.Content.ReadAsAsync<Rental>().Result;
				TempData["SuccessMessage"] = "Main Rental record created successfully.";
				// get the Rental Items based on the RentalId of the model Rental
				response = WebClient.ApiClient.GetAsync($"RentalItemsById/{rental.RentalID}").Result;
				IList<RentalItem> rentalItems = response.Content.ReadAsAsync<List<RentalItem>>().Result;

				// check if Rental Items is empty
				if (rentalItems.Count == 0)
					return RedirectToAction("Edit", new { Id = rental.RentalID });
				else
					return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Rental/Edit/5
		public ActionResult Edit(int Id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Rentals/{Id}").Result;
			var rental = response.Content.ReadAsAsync<Rental>().Result;
			response = WebClient.ApiClient.GetAsync($"RentalItemsById/{Id}").Result;
			IList<RentalItem> rentalItems = response.Content.ReadAsAsync<IList<RentalItem>>().Result;

			response = WebClient.ApiClient.GetAsync("Tools").Result;
			IList<Tool> tools = response.Content.ReadAsAsync<IList<Tool>>().Result;

			rental.Customers = GetCustomers();

			var rentedTools = rentalItems.Select(m => new CustomerToolsViewModel
			{
				RentalItemID = m.RentalItemID,
				RentalID = m.RentalId,
				ToolName = tools.Where(mv => mv.ToolID == m.ToolID).Select(r => r.ToolName).FirstOrDefault()
			}).ToList();

			rental.RentedTools = rentedTools;
			return View(rental);
		}

		// POST: Rental/Edit/5
		[HttpPost]
		public ActionResult Edit(int Id, Rental rental)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"Rentals/{Id}", rental).Result;
				TempData["SuccessMessage"] = "Rental record updated successfully.";
				if (response.IsSuccessStatusCode)
					return RedirectToAction("Index");

				return View(rental);
			}
			catch
			{
				return View();
			}
		}

		// GET: Rental/Delete/5
		public ActionResult Delete(int Id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Rentals/{Id}").Result;
			var rental = response.Content.ReadAsAsync<Rental>().Result;

			response = WebClient.ApiClient.GetAsync("Customers").Result;
			IList<Customer> customers = response.Content.ReadAsAsync<IList<Customer>>().Result;

			response = WebClient.ApiClient.GetAsync("Tools").Result;
			IList<Tool> tools = response.Content.ReadAsAsync<IList<Tool>>().Result;

			var customerRentalDetails = new CustomerRentalDetailsViewModel
			{
				Rental = rental,
				CustomerName = customers.Where(cu => cu.CustomerID == rental.CustomerID).Select(c => c.CustomerName).FirstOrDefault(),
				RentedTools = rental.RentalItems.Select(ri => new CustomerToolsViewModel
				{
					RentalID = ri.RentalId,
					ToolName = tools.Where(m => m.ToolID == ri.ToolID).Select(s => s.ToolName).FirstOrDefault()
				}).ToList()
			};

			return View(customerRentalDetails);
		}

		// POST: Rental/Delete/5
		[HttpPost]
		public ActionResult Delete(int Id, Rental rental)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"Rentals/{Id}").Result;
				TempData["SuccessMessage"] = "Rental record deleted successfully.";
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: AddTools
		public ActionResult AddTools(int RentalId)
		{
			var rentalItem = new RentalItem();
			var tools = GetTools();
			rentalItem.RentalId = RentalId;
			rentalItem.Tools = tools;

			return View(rentalItem);
		}

		// POST: AddTools
		[HttpPost]
		public ActionResult AddTools(RentalItem rentalItem)
		{
			try
			{
				int Id = rentalItem.RentalId;
				HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("RentalItems", rentalItem).Result;
				TempData["SuccessMessage"] = "Tools Rental added successfully.";
				return RedirectToAction("Edit", new { Id });
			}
			catch
			{
				return View("No record of the associated rental can be found.\nMake Sure to submit the Rental details before add the tools");
			}

		}

		// GET: EditRentedTool
		public ActionResult EditRentedTool(int Id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"RentalItems/{Id}").Result;
			var rentalItem = response.Content.ReadAsAsync<RentalItem>().Result;
			rentalItem.Tools = GetTools();

			return View(rentalItem);
		}

		// POST: EditRentedTool
		[HttpPost]
		public ActionResult EditRentedTool(int Id, RentalItem rentalItem)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"RentalItems/{Id}", rentalItem).Result;
				TempData["SuccessMessage"] = "Tool Rental record updated successfully.";
				int rentalId = rentalItem.RentalId;
				if (response.IsSuccessStatusCode)
					return RedirectToAction("Edit", new { id = rentalId });
				return View(rentalItem);
			}
			catch
			{
				return View();
			}
		}

		//GET: DeleteRentedTool
		public ActionResult DeleteRentedTool(int Id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"RentalItems/{Id}").Result;
			var rentalItem = response.Content.ReadAsAsync<RentalItem>().Result;

			response = WebClient.ApiClient.GetAsync("Tools").Result;
			IList<Tool> tools = response.Content.ReadAsAsync<IList<Tool>>().Result;

			

			return View(rentalItem);
		}

		//POST: DeleteRentedTool
		[HttpPost]
		public ActionResult DeleteRentedTool(int Id, RentalItem rentalItem)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"RentalItems/{Id}").Result;
				TempData["SuccessMessage"] = "Tool Rental deleted successfully.";
				var rentalItemDeleted = response.Content.ReadAsAsync<RentalItem>().Result;
				return RedirectToAction("Edit", new { id = rentalItemDeleted.RentalId });
			}
			catch
			{
				return View();
			}
		}

		#region Helper Methods

		public IEnumerable<SelectListItem> GetCustomers()
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Customers").Result;
			IList<Customer> customers = response.Content.ReadAsAsync<IList<Customer>>().Result;
			List<SelectListItem> customerList = customers.OrderBy(o => o.CustomerName).Select(c => new SelectListItem
			{
				Value = c.CustomerID.ToString(),
				Text = c.CustomerName
			}).ToList();
			return new SelectList(customerList, "Value", "Text");
		}

		public IEnumerable<SelectListItem> GetTools()
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Tools").Result;
			IList<Tool> tools = response.Content.ReadAsAsync<IList<Tool>>().Result;
			List<SelectListItem> toolList = tools.OrderBy(o => o.ToolName).Select(m => new SelectListItem
			{
				Value = m.ToolID.ToString(),
				Text = m.ToolName
			}).ToList();
			return new SelectList(toolList, "Value", "Text");
		}
		#endregion
	}
}