using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UQing.Hr.IServices;
using UQing.Hr.Model.Common;
using UQing.Hr.Model.User;
using UQing.Hr.WebHelper;

namespace UQing.Hr.Web.Controllers
{
	/// <summary>
	/// 企业中心控制器
	/// </summary>
	public class CompanyController : BaseController
	{
		public CompanyController(IView_ServerUserInfoServices _View_ServerUserInfoServices
			, IServerUser_PostServices _ServerUser_PostServices)
		{
			base._View_ServerUserInfoServices = _View_ServerUserInfoServices;
			base._ServerUser_PostServices = _ServerUser_PostServices;
		}
		/// <summary>
		/// 企业中心页面
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Index()
		{
			UserManage.JudgeUserIdentityOpt(IdentityType.ServerUser);
			return View();
		}

		/// <summary>
		/// 经纪人发布职位
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult JobAdd()
		{
			UserManage.JudgeUserIdentityOpt(IdentityType.ServerUser);
			return View();
		}
		/// <summary>
		/// 职位管理页面
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult JobList()
		{
			UserManage.JudgeUserIdentityOpt(IdentityType.ServerUser);
			return View();
		}


		/// <summary>
		/// 账号管理页面
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult CompInfo()
		{
			UserManage.JudgeUserIdentityOpt(IdentityType.ServerUser);
			return View();
		}

		/// <summary>
		/// 获取经纪人的信息
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult GetSerUserInfo()
		{
			var userInfo = UserManage.GetCurrentUserInfo();
			if (userInfo == null)
			{
				//未登录
				return GetJson(0, new { flag = 1 });
			}
			if (userInfo.IdentityType == IdentityType.ServerUser)
			{
				var serUserInfo = _View_ServerUserInfoServices.QueryWhere(where => where.SerUserID == userInfo.UserId).FirstOrDefault();
				if (serUserInfo == null)
				{
					//经济人不存在
					return GetJson(2, new { flag = 1 });
				}
				return GetJson(1, new { idt = (int)userInfo.IdentityType, serUserInfo = serUserInfo });
			}
			else
			{
				//身份错误
				return GetJson(0, new { flag = 2 });
			}
		}

		/// <summary>
		/// 获取经纪人的职位列表信息
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult GetPostList(FormCollection forms)
		{
			var userInfo = UserManage.GetCurrentUserInfo();
			if (userInfo == null)
			{
				//未登录
				return GetJson(0, new { flag = 1 });
			}
			if (userInfo.IdentityType == IdentityType.ServerUser)
			{
				PageInfo pageInfo = new PageInfo(forms["pageIndex"], forms["pageSize"]);
				int pageCount = 0;
				int totalCount = 0;
				var serUserInfo = _View_ServerUserInfoServices.QueryWhere(where => where.SerUserID == userInfo.UserId).FirstOrDefault();
				if (serUserInfo == null)
				{
					//经济人不存在
					return GetJson(2, new { flag = 1 });
				}
				var posts = _ServerUser_PostServices.QueryByPage(pageInfo.PageIndex, pageInfo.PageSize, out pageCount, out totalCount
						, itemWhere => itemWhere.SerUserID == serUserInfo.SerUserID
						, itemOrder => itemOrder.CreateTime);
				pageInfo.PageCount = pageCount;
				pageInfo.TotalCount = totalCount;
				return GetJson(1, new { posts = posts, pageInfo = pageInfo });
			}
			else
			{
				//身份错误
				return GetJson(0, new { flag = 2 });
			}
		}
	}
}

