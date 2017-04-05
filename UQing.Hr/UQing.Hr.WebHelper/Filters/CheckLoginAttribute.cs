using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UQing.Hr.WebHelper
{
	using System.Web.Mvc;
	using System.Web;
	using UQing.Hr.Common;
	using UQing.Hr.IServices;
	using Autofac;
	using UQing.Hr.Model;

	/// <summary>
	/// 统一登录验证过滤器
	/// </summary>
	public class CheckLoginAttribute : ActionFilterAttribute
	{
		/// <summary>
		/// 统一验证Session[Keys.UserInfo]，如果为null，则跳转到登录页面
		/// </summary>
		/// <param name="filterContext"></param>
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			//0.0 判断是否有贴[SkipCheckLogin]的特性标签
			if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipCheckLoginAttribute), false))
			{
				return;
			}
			if (filterContext.ActionDescriptor.IsDefined(typeof(SkipCheckLoginAttribute), false))
			{
				return;
			}

			//1.0 判断Session[Keys.UserInfo]是否为null
			if (filterContext.HttpContext.Session[Keys.UserInfo] == null)
			{
				//1.0.1 查询Cookie[Keys.IsMember]是否不为空，如果成立则模拟用户登录，
				//再将用户实体数据存入Session[Keys.UserInfo]中
				if (!string.IsNullOrEmpty(CookieHelper.Get(Keys.UserInfo)))
				{
					//1.1 取出Cookie中存入的Id的值
					var userId = CookieHelper.Get(Keys.UserInfo).DecryptStr().ToInt32();
					//1.2 根据Id查询用户的实体
					//1.2.1 先要从缓存中获取Autofac容器对象
					var container = CacheHelper.GetData<IContainer>(Keys.AutofacContainer);
					//1.2.2 找Autofac容器获取IUserServices接口的具体实现类的对象实例
					//IUserServices userServices = container.Resolve<IUserServices>();

					//1.3 根据userServices结合Id查询数据
					//var userInfo = userServices.QueryWhere(c => c.Id == userId).FirstOrDefault();
					//if (userInfo != null)
					//{
					//	//1.4 将userInfo存入Session中
					//	filterContext.HttpContext.Session[Keys.UserInfo] = userInfo;
					//}
					//else
					//{
					//	ToLoginView(filterContext);
					//}
				}
				else
				{
					//2.0 跳转到登录页面
					//2.0.1 第一种写法
					//filterContext.HttpContext.Response.Redirect("/Account/Login/Index");
					//2.0.2 第二种写法
					//ContentResult cr = new ContentResult();
					//cr.Content = "<script>alert('您未登录，请先登录！');window.location='/Account/Login/Index'</script>";
					//filterContext.Result = cr;

					ToLoginView(filterContext);
				}
			}

			base.OnActionExecuting(filterContext);
		}

		/// <summary>
		/// 跳转到登录页面
		/// </summary>
		/// <param name="filterContext"></param>
		private static void ToLoginView(ActionExecutingContext filterContext)
		{
			//判断当前请求是否为Ajax请求
			bool isAjaxRequest = filterContext.HttpContext.Request.IsAjaxRequest();
			if (isAjaxRequest)
			{
				//Ajax请求，则返回Json格式
				var jsonResult = new JsonResult
				{
					//Data = new { status = (int)Enums.EAjaxState.NotLogin, msg = "您未登录，或者登录已经失效，请重新登陆" },
					Data = new { result = -2, msg = "您未登录，或者登录已经失效，请重新登陆" },
					JsonRequestBehavior = JsonRequestBehavior.AllowGet
				};
				filterContext.Result = jsonResult;
			}
			else
			{
				//浏览器请求
				var viewResult = new ViewResult
				{
					ViewName = "/Areas/Account/Views/Shared/Tip.cshtml"
				};
				filterContext.Result = viewResult;
			}
		}
	}
}
