using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UQing.Hr.Web.Controllers
{
	/// <summary>
	/// 错误页面控制器
	/// </summary>
    public class ErrorController : Controller
    {
       /// <summary>
       /// 404页面
       /// </summary>
       /// <returns></returns>
        public ActionResult NotFound()
        {
            return View();
        }

    }
}
