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
			string newStr = "";
			if (string.IsNullOrWhiteSpace(str))
			{
				return newStr;
			}
			string replacePattern = @"insert|update|delete|select/img";
			newStr = Regex.Replace(str, replacePattern, "");
			return newStr;
		}
	}
}
