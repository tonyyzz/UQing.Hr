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
	/// <summary>
	/// 找工作 列表页相关
	/// </summary>
	public class JobsController : BaseController
	{
		public JobsController(
			IView_ServerUser_PostServices _View_ServerUser_PostServices
			, IView_WorkPostFilterInfoServices _View_WorkPostFilterInfoServices)
		{
			base._View_ServerUser_PostServices = _View_ServerUser_PostServices;
			base._View_WorkPostFilterInfoServices = _View_WorkPostFilterInfoServices;
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
		public ActionResult List()
		{
			return View();
		}
		/// <summary>
		/// 获取筛选条件
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Filter()
		{
			var conditions = _View_WorkPostFilterInfoServices.QueryWhere(where => where.Classify == 1);
			var query = from item in conditions
						orderby item.TypeOrderId ascending, item.OrderId ascending
						group item by item.TypeOrderId;
			return GetJson(1, new { list = query });
		}

		/// <summary>
		/// 搜索工作
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Search()
		{
			string key = (HttpContext.Request["key"] ?? "").FilterSensitiveWords();
			string searchTypeStr = HttpContext.Request["searchType"] ?? "";
			int searchTypeInt = 0; int.TryParse(searchTypeStr, out searchTypeInt);
			if (searchTypeInt <= 0 || searchTypeInt > 3)
			{
				return new HttpStatusCodeResult(404);
			}
			UQing.Hr.Common.Enums.SearchType searchType = (UQing.Hr.Common.Enums.SearchType)searchTypeInt;
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
				if (searchType == UQing.Hr.Common.Enums.SearchType.All)
				{
					list = _View_ServerUser_PostServices.QueryByPage(pageInfo.PageIndex, pageInfo.PageSize, out pageCount, out totalCount
						, itemWhere => (itemWhere.PostName.ToUpper().Contains(key.ToUpper()) || itemWhere.Company.ToUpper().Contains(key.ToUpper())) //英文字符忽略大小写
						, itemOrder => itemOrder.SerUserPostID);
				}
				else if (searchType == UQing.Hr.Common.Enums.SearchType.Work)
				{
					list = _View_ServerUser_PostServices.QueryByPage(pageInfo.PageIndex, pageInfo.PageSize, out pageCount, out totalCount
						, itemWhere => itemWhere.PostName.ToUpper().Contains(key.ToUpper())
						, itemOrder => itemOrder.SerUserPostID);
				}
				else if (searchType == UQing.Hr.Common.Enums.SearchType.Company)
				{
					list = _View_ServerUser_PostServices.QueryByPage(pageInfo.PageIndex, pageInfo.PageSize, out pageCount, out totalCount
						, itemWhere => itemWhere.Company.ToUpper().Contains(key.ToUpper())
						, itemOrder => itemOrder.SerUserPostID);
				}
				else
				{
					return new HttpStatusCodeResult(404);
				}
			}
			pageInfo.PageCount = pageCount;
			pageInfo.TotalCount = totalCount;
			return GetJson(1, new { list = list, pageInfo = pageInfo });
		}
	}
}
