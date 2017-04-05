using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UQing.Hr.Model.Common
{
	/// <summary>
	/// 公共分页类
	/// </summary>
	public class PageInfo
	{
		/// <summary>
		/// 公共分页类的无参构造函数
		/// </summary>
		public PageInfo()
		{
			this.PageIndex = 1;
			this.PageSize = 20;
			this.PageCount = 0;
			this.TotalCount = 0;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pageIndex">页码，从1开始</param>
		public PageInfo(int pageIndex)
		{
			this.PageIndex = pageIndex;
			this.PageSize = 20;
			this.PageCount = 0;
			this.TotalCount = 0;
		}
		/// <summary>
		/// 最通用的构造函数
		/// </summary>
		/// <param name="pageIndexStr">页码字符串，1：表示第一页（默认）</param>
		/// <param name="pageSizeStr">每页数量字符串（默认为每页20条数据）</param>
		public PageInfo(string pageIndexStr, string pageSizeStr)
		{
			int pageIndex = 0;
			int pageSize = 0;
			int.TryParse(pageIndexStr, out pageIndex);
			int.TryParse(pageSizeStr, out pageSize);
			if (pageIndex <= 0)
			{
				pageIndex = 1;
			}
			if (pageSize <= 0)
			{
				pageSize = 20;
			}
			this.PageIndex = pageIndex;
			this.PageSize = pageSize;
			this.PageCount = 0;
			this.TotalCount = 0;
		}
		public PageInfo(int pageIndex, int pageSize)
		{
			this.PageIndex = pageIndex;
			this.PageSize = pageSize;
			this.PageCount = 0;
			this.TotalCount = 0;
		}
		/// <summary>
		/// 页码（默认 1：表示第一页）
		/// </summary>
		public int PageIndex { get; set; }
		/// <summary>
		/// 每页数量（默认为每页20条数据）
		/// </summary>
		public int PageSize { get; set; }
		/// <summary>
		/// 总页数
		/// </summary>
		public int PageCount { get; set; }
		/// <summary>
		/// 总数据条数
		/// </summary>
		public int TotalCount { get; set; }
	}
}
