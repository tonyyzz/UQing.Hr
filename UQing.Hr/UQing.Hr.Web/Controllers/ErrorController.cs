using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UQing.Hr.WebHelper;

namespace UQing.Hr.Web.Controllers
{
	/// <summary>
	/// 错误页面控制器
	/// </summary>
	[SkipCheckLogin]
	public class ErrorController : Controller
	{
		/// <summary>
		/// 404页面
		/// </summary>
		/// <returns></returns>
		/// /error/notfound
		[HttpGet]
		public ActionResult NotFound()
		{
			return View();
		}

	}
}
