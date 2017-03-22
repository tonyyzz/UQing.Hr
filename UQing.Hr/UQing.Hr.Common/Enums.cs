using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UQing.Hr.Common
{
    /// <summary>
    /// 枚举管理类 
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// 负责标记Ajax请求以后的Json状态
        /// </summary>
        public enum EAjaxState
        {
            /// <summary>
            /// 成功 0
            /// </summary>
            Success = 0,
            /// <summary>
            /// 错误异常 1
            /// </summary>
            Error = 1,
            /// <summary>
            /// 未登录 2
            /// </summary>
            NotLogin = 2,

            /// <summary>
            /// 不存在 3
            /// </summary>
            UnExists = 3,

            /// <summary>
            /// 不匹配 4
            /// </summary>
            UnMatch = 4,

            /// <summary>
            /// 为空 5
            /// </summary>
            IsNull = 5
        }

        /// <summary>
        /// 负责标记用户的账号可用状态
        /// </summary>
        public enum EState
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal = 0,
            /// <summary>
            /// 停用（删除）
            /// </summary>
            Stop = 1
        }
    }
}
