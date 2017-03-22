using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UQing.Hr.WebHelper
{
    /// <summary>
    /// 跳过登录检查过滤器（自定义）。
    /// 特点：此过滤器只能贴到方法或者类上
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class SkipCheckLoginAttribute : Attribute
    {
    }
}
