using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using UQing.Hr.Common;
using UQing.Hr.IServices;
using UQing.Hr.WebHelper;

namespace UQing.Hr.Web.Controllers
{
	[SkipCheckLogin]
	public class MemberController : BaseController
	{
		public MemberController(IPersonServices _PersonServices
			, IServerUserServices _ServerUserServices)
		{
			base._PersonServices = _PersonServices;
			base._ServerUserServices = _ServerUserServices;
		}
		/// <summary>
		/// 获取用户登录信息
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult UsrInf()
		{
			UQing.Hr.Model.User.UserInfo userInfo = UserManage.GetCurrentUserInfo();
			if (userInfo != null) //未登录
			{
				return GetJson(1, new { name = userInfo.RealName, idt = ((int)userInfo.IdentityType) == 1 ? "p" : "s" });
			}
			else //登录
			{
				return GetJson(0);
			}
		}
		/// <summary>
		/// 用户注销
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Logout()
		{
			CookieHelper.Remove(Keys.UserInfo);
			if (UserManage.SetCurrentUserInfo(null))
			{
				return GetJson(1);
			}
			else
			{
				return GetJson(0);
			}

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
		public ActionResult Login(string username, string pwd, string idt, string autoLogin)
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

			//是否勾选 '7天自动登录' 
			bool isChecked = false;
			if (!string.IsNullOrWhiteSpace(autoLogin) && autoLogin == "1")
			{
				isChecked = true;
			}

			pwd = pwd.ToMd5();

			Model.User.UserInfo userInfo = null;

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
					userInfo = new Model.User.UserInfo()
					{
						IdentityType = Model.User.IdentityType.Person,
						UserId = person.PerID,
						RealName = person.RealName,
						Phone = person.Phne,
						Email = person.Email
					};
					//求职者登录成功
					UserManage.SetCurrentUserInfo(userInfo);

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
					userInfo = new Model.User.UserInfo()
					{
						IdentityType = Model.User.IdentityType.ServerUser,
						UserId = serverUser.SerUserID,
						RealName = serverUser.RealName,
						Phone = serverUser.Phone,
						Email = serverUser.Email
					};
					//求职者登录成功
					UserManage.SetCurrentUserInfo(userInfo);

				}
			}
			else
			{
				//非法操作
				return GetJson(0, new { flag = 3 });
			}

			//登录成功
			//设置cookie值，7天内自动登录
			if (isChecked)
			{
				//字符串连接 '用户名|身份标识'
				string userCookieStr = UserManage.GetUserCookieStr(userInfo.UserId, userInfo.IdentityType);
				CookieHelper.Set(Keys.UserInfo, userCookieStr.EncryptStr(), DateTime.Now.AddDays(7));
			}
			else
			{
				CookieHelper.Remove(Keys.UserInfo);
			}

			return GetJson(1);
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
		/// 用户注册页面（idt，1：求职者用户，2：经纪人用户）
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
		/// <summary>
		/// 个人注册
		/// </summary>
		/// <param name="forms"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Registp(FormCollection forms)
		{
			string username = forms["username"];
			string email = forms["email"];
			string pwd = forms["pwd"];
			string pwdConfirm = forms["pwdConfirm"];
			if (string.IsNullOrWhiteSpace(username) || !Regex.IsMatch(username, @"^([\u4e00-\u9fa5]|[a-zA-Z])([\u4e00-\u9fa5]|[0-9a-zA-Z]){5,17}$"))
			{
				//用户名非法
				return GetJson(0, new { flag = 1 });
			}
			if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$"))
			{
				//邮箱非法
				return GetJson(0, new { flag = 2 });
			}
			if (string.IsNullOrWhiteSpace(pwd) || !Regex.IsMatch(pwd, @"^[\s|\S]{6,16}$"))
			{
				//密码设置非法
				return GetJson(0, new { flag = 3 });
			}
			if (string.IsNullOrWhiteSpace(pwdConfirm) || pwdConfirm != pwd)
			{
				//密码不一致
				return GetJson(0, new { flag = 4 });
			}

			Model.Person person = _PersonServices.QueryWhere(item => item.RealName == username).FirstOrDefault();
			if (person != null)
			{
				//用户名已经被注册，请更换用户名
				return GetJson(2, new { flag = 1 });
			}
			person = _PersonServices.QueryWhere(item => item.Email == email).FirstOrDefault();
			if (person != null)
			{
				//邮箱已经被绑定，请更换邮箱
				return GetJson(2, new { flag = 2 });
			}

			person = new Model.Person()
			{
				RealName = username,
				Email = email,
				Password = pwd.ToMd5(),
				RegTime = DateTime.Now
			};
			_PersonServices.Add(person);
			if (_PersonServices.SaveChanges() > 0) //注册成功
			{
				return GetJson(1);
			}
			else //注册失败
			{
				return GetJson(3);
			}
		}
		/// <summary>
		/// 企业注册
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult RegistS(FormCollection forms)
		{
			string companyName = forms["companyName"];
			string username = forms["username"];
			string phone = forms["phone"];
			string email = forms["email"];
			string pwd = forms["pwd"];
			string pwdConfirm = forms["pwdConfirm"];
			if (string.IsNullOrWhiteSpace(companyName) || !Regex.IsMatch(companyName, @"^[\s|\S]{2,25}$"))
			{
				//企业名称非法
				return GetJson(0, new { flag = 1 });
			}
			if (string.IsNullOrWhiteSpace(username) || !Regex.IsMatch(username, @"^([\u4e00-\u9fa5]|[a-zA-Z])([\u4e00-\u9fa5]|[0-9a-zA-Z]){5,17}$"))
			{
				//用户名非法
				return GetJson(0, new { flag = 2 });
			}
			if (string.IsNullOrWhiteSpace(phone) || !Regex.IsMatch(phone, @"^1\d{10}$"))
			{
				//手机号码非法
				return GetJson(0, new { flag = 3 });
			}
			if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$"))
			{
				//邮箱非法
				return GetJson(0, new { flag = 4 });
			}
			if (string.IsNullOrWhiteSpace(pwd) || !Regex.IsMatch(pwd, @"^[\s|\S]{6,16}$"))
			{
				//密码设置非法
				return GetJson(0, new { flag = 5 });
			}
			if (string.IsNullOrWhiteSpace(pwdConfirm) || pwdConfirm != pwd)
			{
				//密码不一致
				return GetJson(0, new { flag = 6 });
			}

			Model.ServerUser serverUser = _ServerUserServices.QueryWhere(item => item.RealName == username).FirstOrDefault();
			if (serverUser != null)
			{
				//用户名已经被注册，请更换用户名
				return GetJson(2, new { flag = 1 });
			}
			serverUser = _ServerUserServices.QueryWhere(item => item.Phone == phone).FirstOrDefault();
			if (serverUser != null)
			{
				//手机号码已经被注册，请更换手机号码
				return GetJson(2, new { flag = 2 });
			}
			serverUser = _ServerUserServices.QueryWhere(item => item.Email == email).FirstOrDefault();
			if (serverUser != null)
			{
				//邮箱已经被绑定，请更换邮箱
				return GetJson(2, new { flag = 3 });
			}

			serverUser = new Model.ServerUser()
			{
				Company = companyName,
				RealName = username,
				Phone = phone,
				Email = email,
				Password = pwd.ToMd5(),

				RegTime = DateTime.Now,
				Balance = 0,
			};
			_ServerUserServices.Add(serverUser);
			if (_ServerUserServices.SaveChanges() > 0) //注册成功
			{
				return GetJson(1);
			}
			else //注册失败
			{
				return GetJson(3);
			}
		}
		/// <summary>
		/// 找回密码页面
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Find()
		{
			return View();
		}
		/// <summary>
		/// 验证邮箱是否存在
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult ExistEmail(FormCollection forms)
		{
			string idt = forms["idt"];
			string email = forms["email"];
			if (string.IsNullOrWhiteSpace(idt) || !new List<string>() { "p", "s" }.Any(item => item == idt))
			{
				//身份非法
				return GetJson(0, new { flag = 1 });
			}
			if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$"))
			{
				//邮箱非法
				return GetJson(0, new { flag = 2 });
			}
			if (idt == "p")
			{
				//求职者信息
				Model.Person person = _PersonServices.QueryWhere(item => item.Email == email).FirstOrDefault();
				if (person != null)
				{
					//生成随机码
					var guid = Guid.NewGuid().ToString().ToUpper().Trim('-');
					//guid与身份用‘|’分割，并放在Session中
					HttpContext.Session[Keys.VEmailGuidStr] = guid + "|" + idt;
					try
					{
						//发送邮件
						//MailHelper.Send(person.Email, "邮箱测试，这是主题", "这是内容，验证身份为：" + idt + "，验证随机码为：" + guid);
					}
					catch (Exception ex)
					{
						LogHelper.WriteErrorLog("邮箱发送失败", ex);
						return GetJson(3, new { idt = idt });
					}
					//求职者注册，存在该邮箱
					return GetJson(1, new { idt = idt });
				}
				//邮箱未被注册过
				return GetJson(2, new { idt = idt });
			}
			else if (idt == "s")
			{
				//经纪人信息
				Model.ServerUser serverUser = _ServerUserServices.QueryWhere(item => item.Email == email).FirstOrDefault();
				if (serverUser != null)
				{
					//生成随机码
					var guid = Guid.NewGuid().ToString().ToUpper().Trim('-');
					//guid与身份用‘|’分割，并放在Session中
					HttpContext.Session[Keys.VEmailGuidStr] = guid + "|" + idt;
					try
					{
						//发送邮件
						//MailHelper.Send(serverUser.Email, "邮箱测试，这是主题", "这是内容，验证身份为：" + idt + "，验证随机码为：" + guid);
					}
					catch (Exception ex)
					{
						LogHelper.WriteErrorLog("邮箱发送失败", ex);
						return GetJson(3, new { idt = idt });
					}
					//经纪人注册，存在该邮箱
					return GetJson(1, new { idt = idt });
				}
				//邮箱未被注册过
				return GetJson(2, new { idt = idt });
			}
			else
			{
				return new HttpStatusCodeResult(404, "非法操作");
			}
		}
		/// <summary>
		/// 设置新密码
		/// </summary>
		/// <param name="forms"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult NewPwd(FormCollection forms)
		{
			string idt = forms["idt"];
			string vmailcode = forms["vmailcode"];
			string newpwd = forms["newpwd"];
			string theEmail = forms["theEmail"];
			if (string.IsNullOrWhiteSpace(idt) || !new List<string>() { "p", "s" }.Any(item => item == idt))
			{
				//身份非法
				return GetJson(0, new { flag = 1 });
			}
			if (string.IsNullOrWhiteSpace(vmailcode))
			{
				//验证随机码为空
				return GetJson(0, new { flag = 2 });
			}
			if (string.IsNullOrWhiteSpace(newpwd) || !Regex.IsMatch(newpwd, @"^[\s|\S]{6,16}$"))
			{
				//新密码设置非法
				return GetJson(0, new { flag = 3 });
			}
			if (HttpContext.Session[Keys.VEmailGuidStr] == null)
			{
				//验证码为空或者超时
				return GetJson(0, new { flag = 4 });
			}
			if (HttpContext.Session[Keys.VEmailGuidStr].ToString() != string.Join("|", vmailcode, idt))
			{
				//验证随机码错误
				return GetJson(0, new { flag = 5 });
			}
			if (string.IsNullOrWhiteSpace(theEmail) || !Regex.IsMatch(theEmail, @"^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$"))
			{
				//邮箱非法
				return GetJson(0, new { flag = 6 });
			}

			if (idt == "p")
			{
				//求职者信息
				Model.Person person = _PersonServices.QueryWhere(item => item.Email == theEmail).FirstOrDefault();
				if (person != null)
				{
					person.Password = newpwd.ToMd5();
					_PersonServices.Edit(person, new string[] { "Password" });
					if (_PersonServices.SaveChanges() > 0)
					{
						//求职者密码修改成功
						return GetJson(1, new { idt = idt });
					}
					else
					{
						//修改失败
						return GetJson(3, new { idt = idt });
					}
				}
				else
				{
					//求职者信息不存在
					return GetJson(2, new { idt = idt });
				}
			}
			else if (idt == "s")
			{
				//经纪人信息
				Model.ServerUser serverUser = _ServerUserServices.QueryWhere(item => item.Email == theEmail).FirstOrDefault();
				if (serverUser != null)
				{
					serverUser.Password = newpwd.ToMd5();
					_ServerUserServices.Edit(serverUser, new string[] { "Password" });
					if (_ServerUserServices.SaveChanges() > 0)
					{
						//经纪人密码修改成功
						return GetJson(1, new { idt = idt });
					}
					else
					{
						//修改失败
						return GetJson(3, new { idt = idt });
					}
				}
				else
				{
					//经纪人信息不存在
					return GetJson(2, new { idt = idt });
				}
			}
			else
			{
				return new HttpStatusCodeResult(404, "非法操作");
			}
		}
	}
}
