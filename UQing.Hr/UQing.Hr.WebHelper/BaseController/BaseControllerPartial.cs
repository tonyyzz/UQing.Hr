using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UQing.Hr.Common;

namespace UQing.Hr.WebHelper
{
	public partial class BaseController : Controller
	{
		#region 2.0 封装Ajax请求的返回方法
		/// <summary>
		/// 验证成功（0），返回Json数据
		/// </summary>
		/// <param name="successMsg">成功的消息</param>
		/// <returns>Json数据：{status，msg}</returns>
		protected ActionResult WriteSuccess(string successMsg)
		{
			return Json(new { status = (int)Enums.EAjaxState.Success, msg = successMsg });
		}
		/// <summary>
		/// 验证失败（1），返回Json数据
		/// </summary>
		/// <param name="errorMsg">失败的消息</param>
		/// <returns>Json数据：{status，msg}</returns>
		protected ActionResult WriteError(string errorMsg)
		{
			return Json(new { status = (int)Enums.EAjaxState.Error, msg = errorMsg });
		}
		/// <summary>
		/// 验证失败（1），返回Json数据（处理异常信息）
		/// </summary>
		/// <param name="ex">异常（Exception）</param>
		/// <returns>Json数据：{status，msg}</returns>
		protected ActionResult WriteError(Exception ex)
		{
			return Json(new { status = (int)Enums.EAjaxState.Error, msg = ex.Message });
		}

		/// <summary>
		/// 验证不存在（3），返回Json数据
		/// </summary>
		/// <param name="ex">失败的消息</param>
		/// <returns>Json数据：{status，msg}</returns>
		protected ActionResult WriteUnExists(string unExistsMsg)
		{
			return Json(new { status = (int)Enums.EAjaxState.UnExists, msg = unExistsMsg });
		}

		/// <summary>
		/// 验证不匹配（4），返回Json数据
		/// </summary>
		/// <param name="ex">失败的消息</param>
		/// <returns>Json数据：{status，msg}</returns>
		protected ActionResult WriteUnMatch(string unMatchMsg)
		{
			return Json(new { status = (int)Enums.EAjaxState.UnMatch, msg = unMatchMsg });
		}

		/// <summary>
		/// 验证为空（5），返回Json数据
		/// </summary>
		/// <param name="ex">失败的消息</param>
		/// <returns>Json数据：{status，msg}</returns>
		protected ActionResult WriteIsNull(string isNullMsg)
		{
			return Json(new { status = (int)Enums.EAjaxState.IsNull, msg = isNullMsg });
		}

		#endregion
	}
}
