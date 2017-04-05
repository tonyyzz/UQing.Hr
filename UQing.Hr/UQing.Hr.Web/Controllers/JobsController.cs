using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UQing.Hr.IServices;
using UQing.Hr.Model;
using UQing.Hr.Model.Common;
using UQing.Hr.WebHelper;

namespace UQing.Hr.Web.Controllers
{
	public class JobsController : BaseController
	{
		public JobsController(IView_ServerUser_PostServices _View_ServerUser_PostServices)
		{
			base._View_ServerUser_PostServices = _View_ServerUser_PostServices;
		}
		public ActionResult Index()
		{
			return View();
		}
		/// <summary>
		/// 工作列表页面
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult list()
		{
			return View();
		}
		/// <summary>
		/// 搜索工作
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Search()
		{
			string key = (HttpContext.Request["key"] ?? "").FilterSensitiveWords().Trim();
			int pageCount = 0;
			int totalCount = 0;
			PageInfo pageInfo = new PageInfo(HttpContext.Request["pageIndex"], HttpContext.Request["pageSize"]);
			List<View_ServerUser_Post> list = new List<View_ServerUser_Post>();
			if (string.IsNullOrWhiteSpace(key))
			{
				//关键词为空，则获取默认工作列表信息
				list = _View_ServerUser_PostServices.QueryByPage(pageInfo.PageIndex, pageInfo.PageSize, out pageCount, out totalCount
					, null
					, itemOrder => itemOrder.SerUserPostID);
			}
			else
			{
				list = _View_ServerUser_PostServices.QueryByPage(pageInfo.PageIndex, pageInfo.PageSize, out pageCount, out totalCount
					, itemWhere => itemWhere.PostName.ToUpper().Contains(key.ToUpper()) //英文字符忽略大小写
					, itemOrder => itemOrder.SerUserPostID);
			}
			pageInfo.PageCount = pageCount;
			pageInfo.TotalCount = totalCount;
			return GetJson(1, new { list = list, pageInfo = pageInfo });
		}
	}
}
