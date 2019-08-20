using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToolRental_FrontEnd.ViewModels
{
	public class RentedToolsViewModel
	{
		public int RentalID { get; set; }
		public string ToolName { get; set; }
		public string CustomerName { get; set; }
		public DateTime DateRented { get; set; }
		public DateTime? DateReturned { get; set; }
	}
}