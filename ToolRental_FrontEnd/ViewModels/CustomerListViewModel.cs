using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToolRental_FrontEnd.Controllers
{
	public class CustomerListViewModel
	{
		public int CustomerID { get; set; }
		[Display(Name = "Customer Name")]
		public string CustomerName { get; set; }
		[Display(Name = "Safety Induction")]
		public Nullable<bool> SafetyInduction { get; set; }
		[Display(Name = "Workspace Name")]
		public String Workspace { get; set; }
	}
}