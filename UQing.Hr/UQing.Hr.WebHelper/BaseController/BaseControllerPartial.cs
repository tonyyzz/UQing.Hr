using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UQing.Hr.Common;
using UQing.Hr.Model.User;

namespace UQing.Hr.WebHelper
{
	public partial class BaseController : Controller
	{
		/// <summary>
		/// 返回Json数据
		/// </summary>
		/// <param name="status">状态码</param>
		/// <param name="data">数据集</param>
		/// <returns></returns>
		protected JsonResult GetJson(int status, object data = null)
		{
			var result = new { result = status, data = data };
			return Json(result);
		}
	}
}
