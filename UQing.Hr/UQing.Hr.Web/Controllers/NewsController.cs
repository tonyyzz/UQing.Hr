using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UQing.Hr.WebHelper;

namespace UQing.Hr.Web.Controllers
{
	/// <summary>
	/// 新闻资讯
	/// </summary>
	[SkipCheckLogin]
	public class NewsController : BaseController
	{
		/// <summary>
		/// 新闻资讯 首页页面
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// 新闻资讯 分类列表页面
		/// </summary>
		/// <returns></returns>
		public ActionResult List()
		{
			return View();
		}
	}
}
