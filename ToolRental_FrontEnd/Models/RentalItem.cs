using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToolRental_FrontEnd.Models
{
	public class RentalItem
	{
		public int RentalItemID { get; set; }
		public int RentalId { get; set; }
		public int ToolID { get; set; }
		public IEnumerable<SelectListItem> Tools { get; set; }
	}
}