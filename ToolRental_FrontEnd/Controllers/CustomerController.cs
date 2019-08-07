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
			return View(customer);
		}

		// GET: Customer/Details/5
		public ActionResult Details(int id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Customers/{id}").Result;
			var customer = response.Content.ReadAsAsync<Customer>().Result;

			return View(customer);
		}

		// GET: Customer/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Customer/Create
		[HttpPost]
		public ActionResult Create(Customer customer)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("Customers", customer).Result;
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

			return View(customer);
		}

		// POST: Customer/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, Customer customer)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"Customers/{id}", customer).Result;
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

			return View(customer);
		}

		// POST: Customer/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, Customer customer)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"Customers/{id}").Result;
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}
