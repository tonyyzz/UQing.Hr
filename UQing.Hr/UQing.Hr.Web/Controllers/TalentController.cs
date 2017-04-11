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
	/// 招人才 列表页相关
	/// </summary>
	public class TalentController : BaseController
	{
		public TalentController(IView_Person_OrderServices _View_Person_OrderServices)
		{
			base._View_Person_OrderServices = _View_Person_OrderServices;
		}
		/// <summary>
		/// 招人才 界面
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult List()
		{
			return View();
		}
		/// <summary>
		/// 搜索人才
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Search()
		{
			string key = (HttpContext.Request["key"] ?? "").FilterSensitiveWords();
			//string searchTypeStr = HttpContext.Request["searchType"] ?? "";
			//int searchTypeInt = 0; int.TryParse(searchTypeStr, out searchTypeInt);
			//if (searchTypeInt <= 0 || searchTypeInt > 3)
			//{
			//	return Redirect("/error/notfound");
			//}
			//UQing.Hr.Common.Enums.SearchType searchType = (UQing.Hr.Common.Enums.SearchType)searchTypeInt;
			int pageCount = 0;
			int totalCount = 0;
			PageInfo pageInfo = new PageInfo(HttpContext.Request["pageIndex"], HttpContext.Request["pageSize"]);
			List<View_Person_Order> list = new List<View_Person_Order>();
			if (string.IsNullOrWhiteSpace(key))
			{
				//关键词为空，则获取默认工作列表信息
				list = _View_Person_OrderServices.QueryByPage(pageInfo.PageIndex, pageInfo.PageSize, out pageCount, out totalCount
					, null
					, itemOrder => itemOrder.CreateTime);
			}
			else
			{
				list = _View_Person_OrderServices.QueryByPage(pageInfo.PageIndex, pageInfo.PageSize, out pageCount, out totalCount
					, itemWhere => (itemWhere.EngagePost.ToUpper().Contains(key.ToUpper()) || itemWhere.Trade.ToUpper().Contains(key.ToUpper())) //英文字符忽略大小写
					, itemOrder => itemOrder.CreateTime);
			}
			pageInfo.PageCount = pageCount;
			pageInfo.TotalCount = totalCount;
			return GetJson(1, new { list = list, pageInfo = pageInfo });
		}
	}
}
