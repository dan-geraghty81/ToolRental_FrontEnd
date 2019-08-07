using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToolRental_FrontEnd.Models
{
	public class Customer
	{
		public int CustomerID { get; set; }
		public string CustomerName { get; set; }
		public Nullable<bool> SafetyInduction { get; set; }
		public int WorkspaceID { get; set; }
	}
}