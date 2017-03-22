using System;
using System.Web;
using UQing.Hr.Common;

namespace UQing.Hr.WebHelper
{
    /// <summary>
    /// 验证码管理类
    /// </summary>
    public class VCodeMng
    {
        /// <summary>
        /// 判断验证码合法性
        /// </summary>
        /// <param name="vCode">验证码字符串</param>
        /// <returns>true：验证码合法；false：验证码不合法</returns>
        public static bool IsVCodeLegal(string vCode)
        {
            var vCodeFromSession = string.Empty;
            if (HttpContext.Current.Session[Keys.VCode] != null)
            {
                vCodeFromSession = HttpContext.Current.Session[Keys.VCode].ToString();
            }
            else
            {
                return false;
            }
            if (string.IsNullOrEmpty(vCode)
                || vCodeFromSession.Equals(vCode, StringComparison.InvariantCultureIgnoreCase) == false)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取 "找回密码" 步骤中的邮箱验证序列字符串
        /// </summary>
        /// <returns>邮箱验证序列字符串</returns>
        public static string GetVEmailGuidSequenceStr()
        {
            if (HttpContext.Current.Session[Keys.VEmailGuidStr] != null)
            {
                return HttpContext.Current.Session[Keys.VEmailGuidStr].ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取 "注册" 步骤中的邮箱发送验证序列字符串
        /// </summary>
        /// <returns>邮箱验证序列字符串</returns>
        public static string GetRegisterEmailGuidSequenceStr()
        {
            if (HttpContext.Current.Session[Keys.RegisterEmailGuidStr] != null)
            {
                return HttpContext.Current.Session[Keys.RegisterEmailGuidStr].ToString();
            }
            return string.Empty;
        }
    }
}
