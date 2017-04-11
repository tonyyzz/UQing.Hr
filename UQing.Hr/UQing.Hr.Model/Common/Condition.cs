using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UQing.Hr.Model.Common
{
	/// <summary>
	/// 找工作、招人才的筛选条件类
	/// </summary>
	public class Condition
	{
		/// <summary>
		/// 筛选类型id
		/// </summary>
		public int typeid { get; set; }
		/// <summary>
		/// 筛选id集合（以typeid分组）
		/// </summary>
		public string ids { get; set; }
	}
}
