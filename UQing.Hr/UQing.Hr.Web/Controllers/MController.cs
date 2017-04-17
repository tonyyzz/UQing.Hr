using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using UQing.Hr.IServices;
using UQing.Hr.Model.User;
using UQing.Hr.WebHelper;

namespace UQing.Hr.Web.Controllers
{
	/// <summary>
	/// 会员中心控制器
	/// </summary>
	public class MController : BaseController
	{
		public MController(IPersonServices _PersonServices,
			IView_PersonInfoServices _View_PersonInfoServices)
		{
			base._PersonServices = _PersonServices;
			base._View_PersonInfoServices = _View_PersonInfoServices;
		}
		/// <summary>
		/// 会员中心首页
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			UserManage.JudgeUserIdentityOpt(IdentityType.Person);
			return View();
		}

		public ActionResult UserInfo()
		{
			UserManage.JudgeUserIdentityOpt(IdentityType.Person);
			return View();
		}

		/// <summary>
		/// 获取求职者信息
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult GetPerInfo()
		{
			var userInfo = UserManage.GetCurrentUserInfo();
			if (userInfo == null)
			{
				//未登录
				return GetJson(0, new { flag = 1 });
			}
			if (userInfo.IdentityType == IdentityType.Person)
			{
				var person = _PersonServices.QueryWhere(where => where.PerID == userInfo.UserId).FirstOrDefault();
				if (person == null)
				{
					//求职者不存在
					return GetJson(2, new { flag = 1 });
				}
				return GetJson(1, new { idt = (int)userInfo.IdentityType, person = person });
			}
			else
			{
				//身份错误
				return GetJson(0, new { flag = 2 });
			}
		}
		/// <summary>
		/// 获取求职者信息（包含求职者职位信息）
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult GetViewPerInfo()
		{
			var userInfo = UserManage.GetCurrentUserInfo();
			if (userInfo == null)
			{
				//未登录
				return GetJson(0, new { flag = 1 });
			}
			if (userInfo.IdentityType == IdentityType.Person)
			{
				var personInfo = _View_PersonInfoServices.QueryWhere(where => where.PerID == userInfo.UserId).FirstOrDefault();
				if (personInfo == null)
				{
					//求职者不存在
					return GetJson(2, new { flag = 1 });
				}
				return GetJson(1, new { idt = (int)userInfo.IdentityType, personInfo = personInfo });
			}
			else
			{
				//身份错误
				return GetJson(0, new { flag = 2 });
			}
		}
		/// <summary>
		/// 求职者信息修改保存
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SavePerInfo(FormCollection forms)
		{
			var userInfo = UserManage.GetCurrentUserInfo();
			if (userInfo == null)
			{
				//未登录
				return GetJson(-1, new { flag = 1 });
			}
			if (userInfo.IdentityType == IdentityType.Person)
			{
				var person = _PersonServices.QueryWhere(where => where.PerID == userInfo.UserId).FirstOrDefault();
				if (person == null)
				{
					//求职者不存在
					return GetJson(2, new { flag = 1 });
				}
				//保存
				string realname = forms["realname"] ?? "";
				string gender = forms["gender"] ?? "";
				string residence = forms["residence"] ?? "";
				string education = forms["education"] ?? "";
				string workLife = forms["workLife"] ?? "";
				string phone = forms["phone"] ?? "";
				string email = forms["email"] ?? "";
				if (string.IsNullOrWhiteSpace(realname))
				{
					//姓名不能为空
					return GetJson(0, new { flag = 1 });
				}
				if (string.IsNullOrWhiteSpace(gender))
				{
					gender = "1";
				}
				bool sex = true; //默认男
				if (gender == "2")
				{
					sex = false;
				}
				if (string.IsNullOrWhiteSpace(residence))
				{
					residence = "";
				}
				if (string.IsNullOrWhiteSpace(education))
				{
					//学历不能为空
					return GetJson(0, new { flag = 2 });
				}
				if (string.IsNullOrWhiteSpace(workLife))
				{
					//工作经验不能为空
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
				person.RealName = realname;
				person.Sex = sex;
				person.City = residence;
				person.Education = education;
				person.WorkLife = workLife;
				person.Phne = phone;
				person.Email = email;
				var properties = new string[] { "RealName", "Sex", "City", "Education", "WorkLife", "Phne", "Email" };
				_PersonServices.Edit(person, properties);
				if (_PersonServices.SaveChanges() > 0)
				{
					//保存成功
					return GetJson(1);
				}
				else
				{
					//保存失败
					return GetJson(3);
				}
			}
			else
			{
				//身份错误
				return GetJson(2, new { flag = 2 });
			}
		}
	}
}
