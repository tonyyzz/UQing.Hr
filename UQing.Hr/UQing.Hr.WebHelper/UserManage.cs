using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UQing.Hr.WebHelper
{
	using UQing.Hr.Model;
	using System.Web;
	using UQing.Hr.Common;

	/// <summary>
	/// 用户管理类，负责管理用户的相关操作。（包含Session）
	/// </summary>
	public class UserManage
	{
		///<summary>
		///获取当前登录用户的实体对象
		///</summary>
		///<returns></returns>
		public static UQing.Hr.Model.User.UserInfo GetCurrentUserInfo()
		{
			if (HttpContext.Current.Session[Keys.UserInfo] != null)
			{
				return HttpContext.Current.Session[Keys.UserInfo] as UQing.Hr.Model.User.UserInfo;
			}
			return null;
		}
		/// <summary>
		/// 将用户信息保存到Session中
		/// </summary>
		/// <returns></returns>
		public static bool SetCurrentUserInfo(UQing.Hr.Model.User.UserInfo userInfo)
		{
			HttpContext.Current.Session[Keys.UserInfo] = userInfo;
			return true;
		}
	}
}
