using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ToolRental_FrontEnd.Models;
using ToolRental_FrontEnd.ViewModels;

namespace ToolRental_FrontEnd.Controllers
{
    public class ReportController : Controller
    {
		public ActionResult GetToolRentalCountReport()
		{
			IEnumerable<ToolRentalCountReport> toolRentalCountReport = GetToolRentalCountData();
			return View(toolRentalCountReport.OrderBy(m => m.ToolName));
		}

		public ActionResult DrawToolRentalCountChart()
		{
			IEnumerable<ToolRentalCountReport> toolRentalCountReport = GetToolRentalCountData();
			return View(toolRentalCountReport);
		}

		private IEnumerable<ToolRentalCountReport> GetToolRentalCountData()
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Reports/GetToolRentalCountReport").Result;
			IEnumerable<ToolRentalCountReport> movieRentalCountReport = response.Content.ReadAsAsync<IEnumerable<ToolRentalCountReport>>().Result;
			return movieRentalCountReport;
		}

		public ActionResult GetCurrentRentalToolListReport(string criteria)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Reports/GetCurrentRentedToolListReport").Result;
			IEnumerable<CurrentRentedToolReport> toolRentalListReport = response.Content.ReadAsAsync<IEnumerable<CurrentRentedToolReport>>().Result;
			if (string.IsNullOrEmpty(criteria))
			{
				TempData["RentedTools"] = toolRentalListReport;
				return View(toolRentalListReport);
			}
			else
			{
				toolRentalListReport = toolRentalListReport.Where(m => m.ToolName.ToLower().Contains(criteria.ToLower()) ||
															m.CustomerName.ToLower().Contains(criteria.ToLower())).ToList();
				TempData["RentedTools"] = toolRentalListReport;
				return View(toolRentalListReport);
			}
		}

		public ActionResult GetCurrentActiveToolsReport(string criteria)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Reports/GetCurrentActiveToolsReport").Result;
			IEnumerable<CurrentActiveToolsReport> activeToolList = response.Content.ReadAsAsync<IEnumerable<CurrentActiveToolsReport>>().Result;
			if (string.IsNullOrEmpty(criteria))
			{
				TempData["ActiveTools"] = activeToolList;
				return View(activeToolList);
			}
			else
			{
				activeToolList = activeToolList.Where(m => m.ToolName.ToLower().Contains(criteria.ToLower()) ||
															m.Brand.ToLower().Contains(criteria.ToLower())).ToList();
				TempData["ActiveTools"] = activeToolList;
				return View(activeToolList);
			}
		}

		public ActionResult GetRetiredToolListReport(string criteria)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Reports/GetRetiredToolListReport").Result;
			IEnumerable<RetiredToolList> retiredToolListReport = response.Content.ReadAsAsync<IEnumerable<RetiredToolList>>().Result;
			if (string.IsNullOrEmpty(criteria))
			{
				TempData["RetiredTools"] = retiredToolListReport;
				return View(retiredToolListReport);
			}
			else
			{
				retiredToolListReport = retiredToolListReport.Where(m => m.ToolName.ToLower().Contains(criteria.ToLower()) ||
															m.Brand.ToLower().Contains(criteria.ToLower())).ToList();
				TempData["RetiredTools"] = retiredToolListReport;
				return View(retiredToolListReport);
			}
		}


		public void ExportRentedToolData()
		{
			List<CurrentRentedToolReport> rentedTools = TempData["RentedTools"] as List<CurrentRentedToolReport>;
			StringWriter sw = new StringWriter();
			sw.WriteLine("\"ToolID\",\"ToolName\",\"Brand\",\"CustomerName\",\"DateRented\"");
			Response.AddHeader("content-disposition", "attachment;filename=RentedTools.csv");
			Response.ContentType = "application/octet-stream";

			foreach (var rentedTool in rentedTools)
			{
				sw.WriteLine($"{rentedTool.ToolID}, {rentedTool.ToolName}, {rentedTool.Brand}, {rentedTool.CustomerName}, {rentedTool.DateRented}");
			}
			Response.Write(sw.ToString());
			Response.End();
		}

		public void ExportRetiredToolData()
		{
			List<RetiredToolList> retiredTools = TempData["RetiredTools"] as List<RetiredToolList>;
			StringWriter sw = new StringWriter();
			sw.WriteLine("\"ToolID\",\"ToolName\",\"Brand\",\"Comments\"");
			Response.AddHeader("content-disposition", "attachment;filename=RetiredTools.csv");
			Response.ContentType = "application/octet-stream";

			foreach (var retiredTool in retiredTools)
			{
				sw.WriteLine($"{retiredTool.ToolID}, {retiredTool.ToolName}, {retiredTool.Brand}, {retiredTool.Comments}");
			}
			Response.Write(sw.ToString());
			Response.End();
		}

		public void ExportActiveToolData()
		{
			List<CurrentActiveToolsReport> activeTools = TempData["ActiveTools"] as List<CurrentActiveToolsReport>;
			StringWriter sw = new StringWriter();
			sw.WriteLine("\"ToolID\",\"ToolName\",\"Brand\",\"Comments\"");
			Response.AddHeader("content-disposition", "attachment;filename=ActiveTools.csv");
			Response.ContentType = "application/octet-stream";

			foreach (var activeTool in activeTools)
			{
				sw.WriteLine($"{activeTool.ToolID}, {activeTool.ToolName}, {activeTool.Brand}, {activeTool.Comments}");
			}
			Response.Write(sw.ToString());
			Response.End();
		}
	}
}