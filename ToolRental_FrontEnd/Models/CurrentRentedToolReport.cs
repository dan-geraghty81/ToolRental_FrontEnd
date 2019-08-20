using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToolRental_FrontEnd.Models
{
	public class CurrentRentedToolReport
	{
		public int ToolID { get; set; }
		public string ToolName { get; set; }
		public string Brand { get; set; }
		public DateTime DateRented { get; set; }
		public string CustomerName { get; set; }
	}
}