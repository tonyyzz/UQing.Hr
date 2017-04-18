using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using UQing.Hr.IServices;
using UQing.Hr.Model;
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
			, IServerUser_PostServices _ServerUser_PostServices
			, IServerUserServices _ServerUserServices)
		{
			base._View_ServerUserInfoServices = _View_ServerUserInfoServices;
			base._ServerUser_PostServices = _ServerUser_PostServices;
			base._ServerUserServices = _ServerUserServices;
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
		/// 获取公司以及经纪人信息
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult GetCompInfo()
		{
			return GetSerUserInfo();
		}

		/// <summary>
		/// 设置公司及经纪人信息
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SetCompInfo(FormCollection forms)
		{

			var userInfo = UserManage.GetCurrentUserInfo();
			if (userInfo == null)
			{
				//未登录
				return GetJson(0, new { flag = 1 });
			}
			if (userInfo.IdentityType == IdentityType.ServerUser)
			{
				string trade = forms["trade"] ?? "";
				string serName = forms["serName"] ?? "";
				string phone = forms["phone"] ?? "";
				string email = forms["email"] ?? "";
				if (string.IsNullOrWhiteSpace(trade))
				{
					return GetJson(0, new { flag = 1 });
				}
				var tradeList = new List<string>() { "计算机硬件", "计算机软件", "互联网/电子商务"
						, "IT服务/系统集成", "通信/电信", "电子技术/半导体/集成电路"
						, "保险/金融", "贸易/进出口", "快速消费品", "生物/制药/医疗器械"
						, "钢铁/机械 ", "广告/媒体", "医疗·化工", "教育/培训"
						, "交通/运输/物流", "餐饮/酒店/娱乐"
						, "政府/非盈利机构", "中介/专业服务", "不限行业", "其他行业"};
				if (!tradeList.Any(item => item == trade))
				{
					return GetJson(0, new { flag = 2 });
				}
				if (string.IsNullOrWhiteSpace(serName) || !Regex.IsMatch(serName, @"^([\u4e00-\u9fa5]|[a-zA-Z])([\u4e00-\u9fa5]|[0-9a-zA-Z]){5,17}$"))
				{
					//联系人非法
					return GetJson(0, new { flag = 3 });
				}
				if (string.IsNullOrWhiteSpace(phone) || !Regex.IsMatch(phone, @"^1\d{10}$"))
				{
					//手机号码非法
					return GetJson(0, new { flag = 4 });
				}
				if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$"))
				{
					//邮箱非法
					return GetJson(0, new { flag = 5 });
				}
				var serverUser = _ServerUserServices.QueryWhere(item => item.SerUserID == userInfo.UserId).FirstOrDefault();
				if (serverUser == null)
				{
					//经济人不存在
					return GetJson(2, new { flag = 1 });
				}
				serverUser.Trade = trade;
				serverUser.RealName = serName;
				serverUser.Phone = phone;
				serverUser.Email = email;
				var properties = new string[] { "Trade", "RealName", "Phone", "Email" };
				_ServerUserServices.Edit(serverUser, properties);
				if (_ServerUserServices.SaveChanges() > 0)
				{
					return GetJson(1);
				}
				else
				{
					return GetJson(3);
				}
			}
			else
			{
				//身份错误
				return GetJson(0, new { flag = 2 });
			}


			return null;
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
		[HttpPost]
		public ActionResult AddJobOpt(FormCollection forms)
		{
			var userInfo = UserManage.GetCurrentUserInfo();
			if (userInfo == null)
			{
				//未登录
				return GetJson(-1, new { flag = 1 });
			}
			if (userInfo.IdentityType == IdentityType.ServerUser)
			{
				string jobName = forms["jobName"] ?? "";
				string jobclassify = forms["jobclassify"] ?? "";
				string workAddress = forms["workAddress"] ?? "";
				string salary = forms["salary"] ?? "";
				string sellingPoints = forms["sellingPoints"] ?? "";
				string postDuty = forms["postDuty"] ?? "";
				if (string.IsNullOrWhiteSpace(jobName))
				{
					//职位名称必填
					return GetJson(0, new { falg = 1 });
				}
				if (string.IsNullOrWhiteSpace(jobclassify))
				{
					//职位类别必填
					return GetJson(0, new { falg = 2 });
				}
				if (string.IsNullOrWhiteSpace(workAddress))
				{
					//工作地点必填
					return GetJson(0, new { falg = 3 });
				}
				var salaryList = new List<string>() { "3k以下", "3k-5k", "5k-10k", "10k以上" };
				if (string.IsNullOrWhiteSpace(salary) || salaryList.Any(item => item == salary))
				{
					salary = salaryList.First();
				}
				if (string.IsNullOrWhiteSpace(sellingPoints))
				{
					sellingPoints = "";
				}
				if (string.IsNullOrWhiteSpace(postDuty))
				{
					//职位描述必填
					return GetJson(0, new { falg = 4 });
				}

				var serverUser = _ServerUserServices.QueryWhere(where => where.SerUserID == userInfo.UserId).FirstOrDefault();
				if (serverUser == null)
				{
					//经纪人不存在
					return GetJson(2, new { falg = 1 });
				}
				var serverUserPost = new ServerUser_Post()
				{
					SerUserID = serverUser.SerUserID,
					Company = serverUser.Company,
					Trade = jobclassify,
					PostName = jobName,
					PostDuty = postDuty,
					Salary = salary,
					WorkAdress = workAddress,
					CompanyMatching = sellingPoints,
					CreateTime = DateTime.Now,
					SeeCount = 0
				};
				_ServerUser_PostServices.Add(serverUserPost);
				if (_ServerUser_PostServices.SaveChanges() > 0)
				{
					return GetJson(1);

				}
				else
				{
					return GetJson(3);

				}
			}
			else
			{
				//身份错误
				return GetJson(-1, new { flag = 2 });
			}

		}
	}
}

