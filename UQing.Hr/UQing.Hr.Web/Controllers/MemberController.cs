using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UQing.Hr.IServices;
using UQing.Hr.WebHelper;

namespace UQing.Hr.Web.Controllers
{
	public class MemberController : BaseController
	{
		public MemberController(IPersonServices _PersonServices
			, IServerUserServices _ServerUserServices)
		{
			base._PersonServices = _PersonServices;
			base._ServerUserServices = _ServerUserServices;
		}
		/// <summary>
		/// 登录页面
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}
		/// <summary>
		/// 登录操作
		/// </summary>
		/// <param name="username"></param>
		/// <param name="pwd"></param>
		/// <param name="idt"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Login(string username, string pwd, string idt)
		{
			if (string.IsNullOrWhiteSpace(username))
			{
				//手机号/会员名/邮箱 为空
				return GetJson(0, new { flag = 1 });
			}
			if (string.IsNullOrWhiteSpace(pwd))
			{
				//密码 为空
				return GetJson(0, new { flag = 2 });
			}
			if (string.IsNullOrWhiteSpace(idt) || !new List<string>() { "p", "s" }.Any(item => item == idt))
			{
				//非法操作
				return GetJson(0, new { flag = 3 });
			}
			pwd = pwd.ToMd5();
			if (idt == "p") //求职者登录
			{
				Model.Person person = _PersonServices.QueryWhere(item =>
						(item.Phne == username || item.Email == username || item.RealName == username)
						&& item.Password.Equals(pwd, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
				if (person == null)
				{
					//用户名或密码错误
					return GetJson(2, new { flag = 1, idt = idt });
				}
				else
				{
					//求职者登录成功
					UserManage.SetCurrentUserInfo(new Model.User.UserInfo()
					{
						IdentityType = Model.User.IdentityType.Person,
						UserId = person.PerID,
						RealName = person.RealName,
						Phone = person.Phne,
						Email = person.Email
					});
					return GetJson(1);
				}
			}
			else if (idt == "s") //经纪人登录
			{
				Model.ServerUser serverUser = _ServerUserServices.QueryWhere(item =>
						(item.Phone == username || item.Email == username || item.RealName == username)
						&& item.Password.Equals(pwd, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
				if (serverUser == null)
				{
					//用户名或密码错误
					return GetJson(2, new { flag = 1, idt = idt });
				}
				else
				{
					//求职者登录成功
					UserManage.SetCurrentUserInfo(new Model.User.UserInfo()
					{
						IdentityType = Model.User.IdentityType.ServerUser,
						UserId = serverUser.SerUserID,
						RealName = serverUser.RealName,
						Phone = serverUser.Phone,
						Email = serverUser.Email
					});
					return GetJson(1);
				}
			}
			else
			{
				//非法操作
				return GetJson(0, new { flag = 3 });
			}
		}
		/// <summary>
		/// 用户注册类型选择界面
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Register()
		{
			return View();
		}
		/// <summary>
		/// 用户注册（idt，1：求职者用户，2：经纪人用户）
		/// </summary>
		/// <param name="idt"></param>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Regist(string id)
		{
			int idt = 0; int.TryParse(id, out idt);
			if (!new List<int>() { 1, 2 }.Any(item => item == idt))
			{
				return RedirectToAction("register");
			}
			if (idt == 1) //求职者用户
			{
				return View("/Views/Member/RegistP.cshtml");
			}
			else //经纪人用户
			{
				return View("/Views/Member/RegistS.cshtml");
			}
		}

		public ActionResult Getpass()
		{
			return View();
		}

	}
}
