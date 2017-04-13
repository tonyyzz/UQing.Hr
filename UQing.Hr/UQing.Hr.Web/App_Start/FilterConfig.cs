using System.Web;
using System.Web.Mvc;
using UQing.Hr.WebHelper;

namespace UQing.Hr.Web
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			//filters.Add(new HandleErrorAttribute());

			filters.Add(new CheckLoginAttribute());
			filters.Add(new ExceptionAttribute());
		}
	}
}