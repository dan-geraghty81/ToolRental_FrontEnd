using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ToolRental_FrontEnd.Models;

namespace ToolRental_FrontEnd.Controllers
{
    public class WorkspaceController : Controller
    {
		// GET: Workspace
		public ActionResult Index()
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Workspaces").Result;
			IEnumerable<Workspace> workspace = response.Content.ReadAsAsync<IEnumerable<Workspace>>().Result;
			return View(workspace);
		}

		// GET: Workspace/Details/5
		public ActionResult Details(int id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Workspaces/{id}").Result;
			var workspace = response.Content.ReadAsAsync<Workspace>().Result;

			return View(workspace);
		}

		// GET: Workspace/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Workspace/Create
		[HttpPost]
		public ActionResult Create(Workspace workspace)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("Workspaces", workspace).Result;
				TempData["SuccessMessage"] = "Workspace record created successfully.";
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Workspace/Edit/5
		public ActionResult Edit(int id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Workspaces/{id}").Result;
			var workspace = response.Content.ReadAsAsync<Workspace>().Result;

			return View(workspace);
		}

		// POST: Workspace/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, Workspace workspace)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"Workspaces/{id}", workspace).Result;
				TempData["SuccessMessage"] = "Workspace record updated successfully.";
				if (response.IsSuccessStatusCode)
					return RedirectToAction("Index");

				return View(workspace);
			}
			catch
			{
				return View();
			}
		}

		// GET: Workspace/Delete/5
		public ActionResult Delete(int id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Workspaces/{id}").Result;
			var workspace = response.Content.ReadAsAsync<Workspace>().Result;

			return View(workspace);
		}

		// POST: Workspace/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, Workspace workspace)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"Workspaces/{id}").Result;
				TempData["SuccessMessage"] = "Workspace record deleted successfully.";
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}
