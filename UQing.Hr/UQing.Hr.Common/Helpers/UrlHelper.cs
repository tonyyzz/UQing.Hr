using System.Text.RegularExpressions;

namespace UQing.Hr.Common
{
    //Url帮助类
    public class MyUrlHelper
    {
        /// <summary>
        /// 获取网站域名
        /// </summary>
        /// <param name="strHtmlPagePath">网页地址</param>
        /// <returns></returns>
       public static string GetUrlDomainName(string strHtmlPagePath)
        {
            string p = @"(\w+\:\/\/[\w:\.]+)\/";
            Regex reg = new Regex(p, RegexOptions.IgnoreCase);
            Match m = reg.Match(strHtmlPagePath);
            return m.Groups[0].Value;
        }
    }
}
