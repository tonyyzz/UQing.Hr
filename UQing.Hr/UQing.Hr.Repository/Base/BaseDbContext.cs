using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UQing.Hr.Repository
{
    using System.Data.Entity;

    /// <summary>
    /// 自定义EF上下文容器
    /// </summary>
    class BaseDbContext : DbContext
    {
        public BaseDbContext()
			: base("name=wuqingerEntities")
        {
        }
    }
}
