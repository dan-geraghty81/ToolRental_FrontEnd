using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ToolRental_FrontEnd.Models;

namespace ToolRental_FrontEnd.Controllers
{
    public class CustomerController : Controller
    {
		// GET: Customer
		public ActionResult Index()
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Customers").Result;
			IEnumerable<Customer> customer = response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;

			response = WebClient.ApiClient.GetAsync("Workspaces").Result;
			IList<Workspace> workspaces = response.Content.ReadAsAsync<IList<Workspace>>().Result;

			var customerListViewModel = customer.Select(r => new CustomerListViewModel
			{
				CustomerID = r.CustomerID,
				CustomerName = r.CustomerName,
				SafetyInduction = r.SafetyInduction,
				Workspace = workspaces.Where(w => w.WorkspaceID == r.WorkspaceID).Select(u => u.WorkspaceName).FirstOrDefault()
			}).ToList();
			return View(customerListViewModel);
		}

		// GET: Customer/Details/5
		public ActionResult Details(int id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Customers/{id}").Result;
			var customer = response.Content.ReadAsAsync<Customer>().Result;

			customer.Workspace = GetWorkspaces();

			return View(customer);
		}

		// GET: Customer/Create
		public ActionResult Create()
		{
			var customer = new Customer();
			customer.Workspace = GetWorkspaces();
			return View(customer);
		}

		// POST: Customer/Create
		[HttpPost]
		public ActionResult Create(Customer customer)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("Customers", customer).Result;
				TempData["SuccessMessage"] = "Customer record created successfully.";
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Customer/Edit/5
		public ActionResult Edit(int id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Customers/{id}").Result;
			var customer = response.Content.ReadAsAsync<Customer>().Result;

			customer.Workspace = GetWorkspaces();

			return View(customer);
		}

		// POST: Customer/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, Customer customer)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"Customers/{id}", customer).Result;
				TempData["SuccessMessage"] = "Customer record updated successfully.";
				if (response.IsSuccessStatusCode)
					return RedirectToAction("Index");

				return View(customer);
			}
			catch
			{
				return View();
			}
		}

		// GET: Customer/Delete/5
		public ActionResult Delete(int id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Customers/{id}").Result;
			var customer = response.Content.ReadAsAsync<Customer>().Result;

			customer.Workspace = GetWorkspaces();

			return View(customer);
		}

		// POST: Customer/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, Customer customer)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"Customers/{id}").Result;
				TempData["SuccessMessage"] = "Customer record deleted successfully.";
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		public IEnumerable<SelectListItem> GetWorkspaces()
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Workspaces").Result;
			IList<Workspace> workspaces = response.Content.ReadAsAsync<IList<Workspace>>().Result;
			List<SelectListItem> workspaceList = workspaces.OrderBy(o => o.WorkspaceName).Select(w => new SelectListItem
			{
				Value = w.WorkspaceID.ToString(),
				Text = w.WorkspaceName
			}).ToList();
			return new SelectList(workspaceList, "Value", "Text");
		}
	}
}
