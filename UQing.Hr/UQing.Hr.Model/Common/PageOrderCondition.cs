using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UQing.Hr.Model.Common
{
	public class PageOrderCondition<TEntity,TKey>
	{
		public Expression<Func<TEntity, TKey>> order { get; set; }
		public bool IsDesc { get; set; }
	}
}
