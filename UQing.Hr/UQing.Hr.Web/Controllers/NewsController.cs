using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using UQing.Hr.IServices;
using UQing.Hr.Model;
using UQing.Hr.Model.Common;
using UQing.Hr.WebHelper;

namespace UQing.Hr.Web.Controllers
{
	/// <summary>
	/// 新闻资讯
	/// </summary>
	[SkipCheckLogin]
	public class NewsController : BaseController
	{
		public NewsController(INewsTypeServices _NewsTypeServices
			, IView_NewsServices _View_NewsServices)
		{
			base._NewsTypeServices = _NewsTypeServices;
			base._View_NewsServices = _View_NewsServices;
		}
		/// <summary>
		/// 新闻资讯 首页页面
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			return View();
		}
		/// <summary>
		/// 获取所有新闻类型
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult GetNewsType()
		{
			var newsTypeList = _NewsTypeServices.QueryOrderByAsc(null, order => order.NewsTypeID);
			return GetJson(1, new { newsTypeList = newsTypeList });
		}
		/// <summary>
		///获取新闻列表 top n  type
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult GetNewsList(FormCollection forms)
		{
			string key = (forms["key"] ?? "").FilterSensitiveWords();
			string newsTypeIdStr = forms["typeid"] ?? "";
			PageInfo pageInfo = new PageInfo(forms["pageIndex"], forms["pageSize"]);
			int newsTypeId = 0; int.TryParse(newsTypeIdStr, out newsTypeId);
			Expression<Func<View_News, bool>> newsWhere = null;
			if (newsTypeId > 0)
			{
				if (string.IsNullOrWhiteSpace(key))
				{
					newsWhere = where => where.NewsType == newsTypeId;
				}
				else
				{
					newsWhere = where => (where.NewsType == newsTypeId) &&
						(where.Title.ToUpper().Contains(key.ToUpper()) || where.AbsDes.ToUpper().Contains(key.ToUpper()));//英文字符忽略大小写
				}

			}
			else
			{
				if (string.IsNullOrWhiteSpace(key))
				{
					newsWhere = null;
				}
				else
				{
					newsWhere = where => (where.Title.ToUpper().Contains(key.ToUpper()) || where.AbsDes.ToUpper().Contains(key.ToUpper()));//英文字符忽略大小写

				}
			}
			int pageCount = 0;
			int totalCount = 0;
			var newsList = new List<View_News>();
			newsList = _View_NewsServices.QueryByPage(pageInfo.PageIndex, pageInfo.PageSize, out pageCount, out totalCount
						, newsWhere
						, itemOrder => itemOrder.CreateTime);
			pageInfo.PageCount = pageCount;
			pageInfo.TotalCount = totalCount;
			return GetJson(1, new { newsList = newsList, pageInfo = pageInfo });
		}

		/// <summary>
		/// 新闻资讯 分类列表页面
		/// </summary>
		/// <returns></returns>
		public ActionResult List(string id = "")
		{
			//id表示新闻类型Id
			int idInt = 0; int.TryParse(id, out idInt);
			if (idInt <= 0)
			{
				return Redirect("/error/notfound");
			}
			var newsTypeInfo = _NewsTypeServices.QueryWhere(where => where.NewsTypeID == idInt).FirstOrDefault();
			if (newsTypeInfo == null)
			{
				return Redirect("/error/notfound");
			}
			return View();
		}
	}
}
