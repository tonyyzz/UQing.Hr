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
	using UQing.Hr.Model.Common;
	/// <summary>
	/// 注：MVC中控制器类的对象是 DefaultControllerFactory（默认控制器工厂类）
	/// DefaultControllerFactory：只会查找默认的无参构造函数，所以此处加了有参的构造函数以后，
	/// DefaultControllerFactory在运行的时候报错
	/// 将来只能由Autofac控制器工厂调用有参的构造函数来创建具体的控制器类实例
	/// </summary>
	[SkipCheckLogin]
	public class HomeController : BaseController
	{
		public HomeController(IView_NewsestRecruitServices _View_NewsestRecruitServices
			, INewsTypeServices _NewsTypeServices
			, INewsServices _NewsServices
			, IView_ServerUser_PostServices _View_ServerUser_PostServices)
		{
			base._View_NewsestRecruitServices = _View_NewsestRecruitServices;
			base._NewsTypeServices = _NewsTypeServices;
			base._NewsServices = _NewsServices;
			base._View_ServerUser_PostServices = _View_ServerUser_PostServices;
		}
		/// <summary>
		/// 首页入口
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Index()
		{
			#region 用户登录信息
			var userInfo = UserManage.GetCurrentUserInfo();
			ViewBag.userInfo = userInfo;
			#endregion

			#region 热门职位
			int pageCount_hot = 0;
			int totalCount_hot = 0;
			PageInfo pageInfo_HotPost = new PageInfo(1, 3);
			var hotPosts = _View_ServerUser_PostServices.QueryByPage(pageInfo_HotPost.PageIndex, pageInfo_HotPost.PageSize
				, out pageCount_hot, out totalCount_hot
				, null,
				order => order.SeeCount);
			ViewBag.hotPosts = hotPosts;
			#endregion

			#region 最新招聘
			PageInfo pageInfo = new PageInfo();
			int pageCount = 0;
			int totalCount = 0;
			var newsestRecruits = _View_NewsestRecruitServices.QueryByPage(pageInfo.PageIndex, pageInfo.PageSize
				, out pageCount, out totalCount
				, null,
				order => order.CreateTime);
			ViewBag.newsestRecruits = newsestRecruits;
			#endregion

			#region 新闻资讯 类型
			var newsTypeList = _NewsTypeServices.QueryOrderByAsc(null, order => order.NewsTypeID);
			ViewBag.newsTypeList = newsTypeList;
			#endregion

			return View();
		}
		/// <summary>
		/// 换一批最新招聘
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult ChangeRecruit(FormCollection forms)
		{
			PageInfo pageInfo = new PageInfo(forms["pageIndex"], forms["pageSize"]);
			int pageCount = 0;
			int totalCount = 0;
			var newsestRecruits = _View_NewsestRecruitServices.QueryByPage(pageInfo.PageIndex, pageInfo.PageSize
				, out pageCount, out totalCount
				, null,
				order => order.CreateTime);
			pageInfo.PageCount = pageCount;
			pageInfo.TotalCount = totalCount;
			return GetJson(1, new { newsestRecruits = newsestRecruits, pageInfo = pageInfo });
		}

	}
}
