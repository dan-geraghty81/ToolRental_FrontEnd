using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolRental_FrontEnd.ViewModels;

namespace ToolRental_FrontEnd.Models
{
	public class Rental
	{
		public int RentalID { get; set; }
		public int CustomerID { get; set; }
		public System.DateTime DateRented { get; set; }
		public Nullable<System.DateTime> DateReturned { get; set; }
		public virtual ICollection<RentalItem> RentalItems { get; set; }
		public IEnumerable<SelectListItem> Customers { get; set; }
		public IEnumerable<CustomerToolsViewModel> RentedTools { get; set; }
	}
}