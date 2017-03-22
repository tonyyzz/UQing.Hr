using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UQing.Hr.Common
{
    /// <summary>
    /// 日志记录帮助类
    /// </summary>
    public class LogHelper
    {
        private static readonly log4net.ILog LogError = log4net.LogManager.GetLogger("logerror");   //选择<logger name="logerror">的配置 
        private static readonly log4net.ILog LogInfo = log4net.LogManager.GetLogger("loginfo");   //选择<logger name="loginfo">的配置 
        private static readonly log4net.ILog LogWarn = log4net.LogManager.GetLogger("logwarn");   //选择<logger name="logwarn">的配置 

        static LogHelper()
        {
            SetConfig();
        }
        /// <summary>
        /// 默认配置。按配置文件
        /// </summary>
        private static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="info">错误消息</param>
        /// <param name="exp">异常信息</param>
        public static void WriteErrorLog(string info, Exception exp)
        {
            if (LogError.IsErrorEnabled)
            {
                LogError.Error(info, exp);
            }
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="exp">异常信息</param>
        public static void WriteErrorLog(Exception exp)
        {
            if (LogError.IsErrorEnabled)
            {
                LogError.Error(exp);
            }
        }

        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="exp">异常信息</param>
        public static void WriteInfoLog(string info, Exception exp)
        {
            if (LogInfo.IsInfoEnabled)
            {
                LogInfo.Info(info, exp);
            }
        }

        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="exp">异常信息</param>
        public static void WriteInfoLog(Exception exp)
        {
            if (LogInfo.IsInfoEnabled)
            {
                LogInfo.Info(exp);
            }
        }

        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="info">警告信息</param>
        /// <param name="exp">异常信息</param>
        public static void WriteWarnLog(string info, Exception exp)
        {
            if (LogWarn.IsWarnEnabled)
            {
                LogWarn.Warn(info, exp);
            }
        }

        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="exp">异常信息</param>
        public static void WriteWarnLog(Exception exp)
        {
            if (LogWarn.IsWarnEnabled)
            {
                LogWarn.Warn(exp);
            }
        }
    }
}
