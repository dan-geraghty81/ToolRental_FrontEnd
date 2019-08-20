using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToolRental_FrontEnd.Models
{
	public class CurrentActiveToolsReport
	{
		public int ToolID { get; set; }
		public string ToolName { get; set; }
		public string Brand { get; set; }
		public string Comments { get; set; }
	}
}