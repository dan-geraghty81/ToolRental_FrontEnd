using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToolRental_FrontEnd.ViewModels
{
	public class RentalListViewModel
	{
		public int RentalID { get; set; }
		public DateTime DateRented { get; set; }
		public DateTime? DateReturned { get; set; }
		public String CustomerName { get; set; }
	}
}