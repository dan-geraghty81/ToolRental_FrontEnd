﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToolRental_FrontEnd.Models
{
	public class Tool
	{
		public int ToolID { get; set; }
		[Display(Name = "Tool Name")]
		public string ToolName { get; set; }
		public string Brand { get; set; }
		public Nullable<bool> Inactive { get; set; }
		public string Comments { get; set; }
		public Nullable<bool> Rented { get; set; }
		public string PicFileName { get; set; }
	}
}