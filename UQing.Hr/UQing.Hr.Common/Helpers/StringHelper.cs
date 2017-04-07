using System.Text.RegularExpressions;
namespace System
{
	/// <summary>
	/// 字符串帮助类
	/// </summary>
	public static class StringHelper
	{
		/// <summary>
		/// 敏感词汇过滤
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string FilterSensitiveWords(this string str)
		{
			if (string.IsNullOrWhiteSpace(str))
			{
				return "";
			}
			return str.ToLower().Trim()
				.Replace("'", "")
				.Replace(";", "")
				.Replace("*", "")
				.Replace("%|", "")
				.Replace("exec", "")
				.Replace("insert", "")
				.Replace("select", "")
				.Replace("delete", "")
				.Replace("update", "")
				.Replace("count", "")
				.Replace("chr", "")
				.Replace("master", "")
				.Replace("truncate", "")
				.Replace("char", "")
				.Replace("declare", "")
				.Replace("script", "")
				.Replace("cast", "")
				.Replace("drop", "");
		}
	}
}
