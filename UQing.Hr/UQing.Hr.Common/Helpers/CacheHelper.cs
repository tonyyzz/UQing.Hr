using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UQing.Hr.Common
{
    using System.Web;

    /// <summary>
    /// 缓存帮助类
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 根据cacheKey获取缓存对象
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static T GetData<T>(string cacheKey)
        {
            return (T)HttpRuntime.Cache[cacheKey];
        }
        /// <summary>
        /// 存入的数据不过期（在IIS重启的时候才消失）
        /// </summary>
        /// <typeparam name="T">要存储的类型</typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="cacheValue"></param>
        public static void SetData<T>(string cacheKey, T cacheValue)
        {
            HttpRuntime.Cache[cacheKey] = cacheValue;
        }
    }
}
