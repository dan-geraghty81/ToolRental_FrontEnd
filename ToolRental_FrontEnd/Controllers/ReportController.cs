using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ToolRental_FrontEnd.Models;

namespace ToolRental_FrontEnd.Controllers
{
    public class ReportController : Controller
    {
		public ActionResult GetToolRentalCountReport()
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Reports/GetToolRentalCountReport").Result;
			IEnumerable<ToolRentalCountReport> toolRentalCountReport = response.Content.ReadAsAsync<IEnumerable<ToolRentalCountReport>>().Result;
			return View(toolRentalCountReport.OrderBy(m => m.ToolName));
		}
    }
}