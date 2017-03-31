using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UQing.Hr.Model.User
{
	/// <summary>
	/// 通用用户信息类
	/// </summary>
	public class UserInfo
	{
		/// <summary>
		/// 通用用户信息类
		/// </summary>
		public UserInfo() { }

		/// <summary>
		/// 用户身份类型
		/// </summary>
		public IdentityType IdentityType { get; set; }
		/// <summary>
		/// 用户Id
		/// </summary>
		public int UserId { get; set; }
		/// <summary>
		/// 用户名
		/// </summary>
		public string RealName { get; set; }
		/// <summary>
		/// 电话号码
		/// </summary>
		public string Phone { get; set; }
		/// <summary>
		/// 邮箱
		/// </summary>
		public string Email { get; set; }
	}
	/// <summary>
	/// 用户身份类型
	/// </summary>
	public enum IdentityType
	{
		/// <summary>
		/// 求职者
		/// </summary>
		Person = 1,
		/// <summary>
		/// 经纪人
		/// </summary>
		ServerUser = 2
	}
}
