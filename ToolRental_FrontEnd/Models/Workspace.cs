using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToolRental_FrontEnd.Models
{
	public class Workspace
	{
		public int WorkspaceID { get; set; }
		[Display(Name = "Workspace Name")]
		public string WorkspaceName { get; set; }
	}
}