using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UQing.Hr.Common
{
    /// <summary>
    /// Key-Value键值对 管理类
    /// </summary>
    public class Keys
    {
        /// <summary>
        /// 用于存放验证码的 Session Key
        /// </summary>
        public const string VCode = "mallvcode";

        /// <summary>
        /// 用于存放登录成功的用户实体的 Session Key
        /// </summary>
        public const string UserInfo = "malluinfo";

        /// <summary>
        /// 用于存放登录成功以后的用户Id的 Cookie Key
        /// </summary>
        public const string IsMember = "mallIsMember";

        /// <summary>
        /// 用于缓存整个Autofac的容器对象的 Cache Key
        /// </summary>
        public const string AutofacContainer = "mallAutofacContainer";

        /// <summary>
        /// 用于 "找回密码" 时验证的GUID字符串 Session Key
        /// </summary>
        public const string VEmailGuidStr = "mallVEmailGuidStr";

        /// <summary>
        /// 用于 "注册" 时邮箱验证的GUID字符串 Session Key
        /// </summary>
        public const string RegisterEmailGuidStr = "mallRegisterEmailGuidStr";
    }
}
