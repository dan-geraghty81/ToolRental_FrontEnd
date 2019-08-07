using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ToolRental_FrontEnd.Models;

namespace ToolRental_FrontEnd.ViewModels
{
	public class CustomerRentalDetailsViewModel
	{
		public Rental Rental { get; set; }
		[Display(Name = "Customer Name")]
		public String CustomerName { get; set; }
		public List<CustomerToolsViewModel> RentedTools { get; set; }
	}
}