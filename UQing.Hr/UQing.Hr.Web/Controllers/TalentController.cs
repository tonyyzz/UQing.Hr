using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
	[SkipCheckLogin]
	public class TalentController : BaseController
	{
		public TalentController(IView_Person_OrderServices _View_Person_OrderServices
			, IView_PersonInfoServices _View_PersonInfoServices)
		{
			base._View_Person_OrderServices = _View_Person_OrderServices;
			base._View_PersonInfoServices = _View_PersonInfoServices;
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

		/// <summary>
		/// 人才详情页面
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Show(string id = "")
		{
			//id为求职者id
			int idInt = 0; int.TryParse(id, out idInt);
			if (idInt <= 0)
			{
				return Redirect("/error/notfound");
			}
			var personInfo = _View_PersonInfoServices.QueryWhere(where => where.PerID == idInt).FirstOrDefault();
			if (personInfo == null)
			{
				return Redirect("/error/notfound");
			}
			return View();
		}

		/// <summary>
		/// 获取求职者的求职信息的接口
		/// </summary>
		/// <param name="forms"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult GetPerInfo(FormCollection forms)
		{
			string perIdSre = forms["perId"] ?? "";
			int perId = 0; int.TryParse(perIdSre, out perId);
			var personInfo = _View_PersonInfoServices.QueryWhere(where => where.PerID == perId).FirstOrDefault();
			if (personInfo == null)
			{
				//求职者信息不存在
				return GetJson(0, new { flag = 1 });
			}
			//联系方式的加密处理
			if (!string.IsNullOrWhiteSpace(personInfo.Phne))
			{
				personInfo.Phne = Regex.Replace(personInfo.Phne, @"(\d{3})(\d{4})(\d{4})", "$1****$3");
			}
			if (!string.IsNullOrWhiteSpace(personInfo.Email))
			{
				personInfo.Email = Regex.Replace(personInfo.Email, @"(^[\S]?)([\S]+?)([\S]?\@[\S]+)", "$1****$3");
			}
			return GetJson(1, new { personInfo = personInfo });
		}
	}
}
