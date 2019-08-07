using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ToolRental_FrontEnd
{
	public class WebClient
	{
		public static HttpClient ApiClient = new HttpClient();

		static WebClient()
		{
			ApiClient.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"]);
		}
	}
}