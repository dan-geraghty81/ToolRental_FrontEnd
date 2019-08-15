using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ToolRental_FrontEnd.Models;

namespace ToolRental_FrontEnd.Controllers
{
    public class ToolController : Controller
    {
		// GET: Tool
		public ActionResult Index()
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Tools").Result;

			// We are using IEnumberable because we only want to enumerate
			// the collection and we are not going to add or delete any
			// elements
			IEnumerable<Tool> tools = response.Content.ReadAsAsync<IEnumerable<Tool>>().Result;
			return View(tools);
		}

		// Edit - Get
		public ActionResult Edit(int id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Tools/{id}").Result;
			// get a tool based on id 
			var tool = response.Content.ReadAsAsync<Tool>().Result;

			// return the tool object to the View
			return View(tool);
		}

		// Edit - Post
		[HttpPost]
		public ActionResult Edit(int id, Tool tool)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"Tools/{id}", tool).Result;
				TempData["SuccessMessage"] = "Tool record updated successfully.";
				if (response.IsSuccessStatusCode)
					return RedirectToAction("Index");

				return View(tool);
			}
			catch
			{
				return View();
			}
		}

		public ActionResult Details(int id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Tools/{id}").Result;
			var tool = response.Content.ReadAsAsync<Tool>().Result;
			return View(tool);
		}

		public ActionResult Delete(int id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Tools/{id}").Result;
			var tool = response.Content.ReadAsAsync<Tool>().Result;
			return View(tool);
		}

		[HttpPost]
		public ActionResult Delete(int id, Tool tool)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"Tools/{id}").Result;
				TempData["SuccessMessage"] = "Tool deleted successfully.";
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// Create - Get
		public ActionResult Create()
		{
			return View();
		}

		// Create - Post
		[HttpPost]
		public ActionResult Create(Tool tool)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("Tools", tool).Result;
				TempData["SuccessMessage"] = "Tool created successfully.";
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		[HttpPost]
		public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
		{
			foreach (var file in files)
			{
				file.SaveAs(Path.Combine(Server.MapPath("~/UploadedFiles"), file.FileName));
			}

			return Json("File uploaded successfully!");
		}

		public ActionResult GoToReport()
		{
			return RedirectToAction("GetRentalCountData", "Report");
		}
	}
}