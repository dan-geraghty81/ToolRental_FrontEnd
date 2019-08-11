using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolRental_FrontEnd.ViewModels;

namespace ToolRental_FrontEnd.Models
{
	public class Rental
	{
		public int RentalID { get; set; }
		[Display(Name = "Customer Name")]
		public int CustomerID { get; set; }
		[Display(Name = "Date Rented")]
		public System.DateTime DateRented { get; set; }
		[Display(Name = "Date Returned")]
		public Nullable<System.DateTime> DateReturned { get; set; }
		[Display(Name = "Rented Tools")]
		public virtual ICollection<RentalItem> RentalItems { get; set; }
		public IEnumerable<SelectListItem> Customers { get; set; }
		[Display(Name = "Rented Tools")]
		public IEnumerable<CustomerToolsViewModel> RentedTools { get; set; }
	}
}