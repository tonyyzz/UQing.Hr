using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UQing.Hr.Web.Controllers
{
	using UQing.Hr.Model;
	using UQing.Hr.IServices;
	using UQing.Hr.WebHelper;
	/// <summary>
	/// 注：MVC中控制器类的对象是 DefaultControllerFactory（默认控制器工厂类）
	/// DefaultControllerFactory：只会查找默认的无参构造函数，所以此处加了有参的构造函数以后，
	/// DefaultControllerFactory在运行的时候报错
	/// 将来只能由Autofac控制器工厂调用有参的构造函数来创建具体的控制器类实例
	/// </summary>
	[SkipCheckLogin]
	public class HomeController : BaseController
	{
		public HomeController(IPersonServices testServices)
		{
			//base._PersonServices = testServices;
			//base._userServices = userServices;
		}
		[HttpGet]
		public ActionResult Index()
		{
			//var ser = _uqinger_article_propertyServices
			//	.QueryWhere(a => a.id > 1);
			//return Json(ser, JsonRequestBehavior.AllowGet);
			return View();
		}

	}
}
