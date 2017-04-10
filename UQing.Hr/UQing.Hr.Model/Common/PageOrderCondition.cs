using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UQing.Hr.Model.Common
{
	/// <summary>
	/// order条件公共类
	/// </summary>
	/// <typeparam name="TEntity">实体类</typeparam>
	/// <typeparam name="TKey">order的字段</typeparam>
	public class PageOrderCondition<TEntity, TKey> where TEntity : class
	{
		/// <summary>
		/// order条件（lambda表达式）
		/// </summary>
		public Expression<Func<TEntity, TKey>> order { get; set; }
		/// <summary>
		/// 是否是倒序
		/// </summary>
		public bool IsDesc { get; set; }
	}
}
