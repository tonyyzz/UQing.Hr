using Newtonsoft.Json;
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
	/// 找工作 列表页相关
	/// </summary>
	[SkipCheckLogin]
	public class JobsController : BaseController
	{
		public JobsController(
			IView_ServerUser_PostServices _View_ServerUser_PostServices
			, IView_WorkPostFilterInfoServices _View_WorkPostFilterInfoServices
			, IServerUserServices _ServerUserServices
			, IView_CompnayInfoServices _View_CompnayInfoServices
			, IServerUser_PostServices _ServerUser_PostServices
			)
		{
			base._View_ServerUser_PostServices = _View_ServerUser_PostServices;
			base._View_WorkPostFilterInfoServices = _View_WorkPostFilterInfoServices;
			base._ServerUserServices = _ServerUserServices;
			base._View_CompnayInfoServices = _View_CompnayInfoServices;
			base._ServerUser_PostServices = _ServerUser_PostServices;
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
			string conditionsStr = HttpContext.Request["conditions"] ?? "";
			if (!string.IsNullOrWhiteSpace(conditionsStr))
			{
				List<UQing.Hr.Model.Common.Condition> conditionList = JsonConvert.DeserializeObject<List<UQing.Hr.Model.Common.Condition>>(conditionsStr);
				if (conditionList != null && conditionList.Any())
				{
					foreach (var item in conditionList)
					{

					}
				}
			}
			string key = (HttpContext.Request["key"] ?? "").FilterSensitiveWords();
			string searchTypeStr = HttpContext.Request["searchType"] ?? "";
			PageInfo pageInfo = new PageInfo(HttpContext.Request["pageIndex"], HttpContext.Request["pageSize"]);
			int searchTypeInt = 0; int.TryParse(searchTypeStr, out searchTypeInt);
			if (searchTypeInt <= 0 || searchTypeInt > 3)
			{
				return Redirect("/error/notfound");
			}
			UQing.Hr.Common.Enums.SearchType searchType = (UQing.Hr.Common.Enums.SearchType)searchTypeInt;
			int pageCount = 0;
			int totalCount = 0;
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
					return Redirect("/error/notfound");
				}
			}
			pageInfo.PageCount = pageCount;
			pageInfo.TotalCount = totalCount;
			return GetJson(1, new { list = list, pageInfo = pageInfo });
		}
		/// <summary>
		/// 找工作的公司详情页面
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Company(string id)
		{
			int idInt = 0; int.TryParse(id, out idInt);
			if (idInt <= 0)
			{
				return Redirect("/error/notfound");
			}
			var serUser = _ServerUserServices.QueryWhere(where => where.SerUserID == idInt).FirstOrDefault();
			if (serUser == null)
			{
				return Redirect("/error/notfound");
			}
			return View();
		}

		/// <summary>
		/// 获取公司信息接口
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult GetCmpny(FormCollection forms)
		{
			string serUserIdStr = forms["serUserId"] ?? "";
			int serUserId = 0; int.TryParse(serUserIdStr, out serUserId);
			if (serUserId <= 0)
			{
				return Redirect("/error/notfound");
			}
			var companyInfo = _View_CompnayInfoServices.QueryWhere(where => where.SerUserID == serUserId).FirstOrDefault();
			if (companyInfo == null)
			{
				return Redirect("/error/notfound");
			}
			//登录与不登录做区分（手机、邮箱的展示）
			var userInfo = UserManage.GetCurrentUserInfo();
			if (userInfo == null)
			{
				if (!string.IsNullOrWhiteSpace(companyInfo.Phone))
				{
					companyInfo.Phone = Regex.Replace(companyInfo.Phone, @"(\d{3})(\d{4})(\d{4})", "$1****$3");
				}
				if (!string.IsNullOrWhiteSpace(companyInfo.Email))
				{
					companyInfo.Email = Regex.Replace(companyInfo.Email, @"(^[\S]?)([\S]+?)([\S]?\@[\S]+)", "$1****$3");
				}
			}
			return GetJson(1, new { companyInfo = companyInfo });
		}
		/// <summary>
		/// 获取公司的职位接口
		/// </summary>
		/// <param name="forms"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult GetCmpnyPost(FormCollection forms)
		{
			string serUserIdStr = forms["serUserId"] ?? "";
			int serUserId = 0; int.TryParse(serUserIdStr, out serUserId);
			PageInfo pageInfo = new PageInfo(forms["pageIndex"], forms["pageSize"]);
			int pageCount = 0;
			int totalCount = 0;
			var postLi = _ServerUser_PostServices.QueryByPage(pageInfo.PageIndex, pageInfo.PageSize, out pageCount, out totalCount
				, where => where.SerUserID == serUserId
				, order => order.SerUserPostID);
			pageInfo.PageCount = pageCount;
			pageInfo.TotalCount = totalCount;
			return GetJson(1, new { postLi = postLi, pageInfo = pageInfo });
		}
		/// <summary>
		/// 显示某一个工作信息的页面
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Show(string id = "")
		{
			//id为职位id
			int idInt = 0; int.TryParse(id, out idInt);
			if (idInt <= 0)
			{
				return Redirect("/error/notfound");
			}
			var postInfo = _View_ServerUser_PostServices.QueryWhere(where => where.SerUserPostID == idInt).FirstOrDefault();
			if (postInfo == null)
			{
				return Redirect("/error/notfound");
			}
			return View();
		}
		/// <summary>
		/// 获取单个工作详情
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult GetJob(FormCollection forms)
		{
			string postIdStr = forms["postid"] ?? "";
			int postId = 0; int.TryParse(postIdStr, out postId);
			if (postId <= 0)
			{
				//非法
				return GetJson(0, new { flag = 1 });
			}
			var postInfo = _View_ServerUser_PostServices.QueryWhere(where => where.SerUserPostID == postId).FirstOrDefault();
			if (postInfo == null)
			{
				//不存在
				return GetJson(0, new { flag = 2 });
			}
			var post = _ServerUser_PostServices.QueryWhere(item => item.SerUserPostID == postInfo.SerUserPostID).FirstOrDefault();
			if (post != null)
			{
				if (post.SeeCount == null || post.SeeCount < 0)
				{
					post.SeeCount = 0;
				}
				else
				{
					post.SeeCount++;
				}
				_ServerUser_PostServices.Edit(post, new string[] { "SeeCount" });
				_ServerUser_PostServices.SaveChanges();
				postInfo.SeeCount = post.SeeCount;
			}
			return GetJson(1, new { postInfo = postInfo });
		}
	}
}
