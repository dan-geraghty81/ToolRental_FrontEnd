using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToolRental_FrontEnd.Models
{
	public class Tool
	{
		public int ToolID { get; set; }
		public string ToolName { get; set; }
		public string Brand { get; set; }
		public Nullable<bool> Inactive { get; set; }
		public string Comments { get; set; }
		public Nullable<bool> Rented { get; set; }
	}
}